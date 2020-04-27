import datetime
import jwt
from flask_jwt_extended import create_access_token , decode_token , get_raw_jwt , get_jwt_identity
from flask_restful import Resource
from flask import request , jsonify , make_response
from model.users import User
from common.app_init import app
from common.utils import auth_required
from common.error import ErrorCode
from common.responses import error_code , unauthorized , success , error_message

class SessionTable:
    __MAX_SESSIONS_PER_USER = 3
    
    __authentication_table = dict({})

    @staticmethod
    def add(id_user,token_id,exp_date):
        
        if not id_user in SessionTable.__authentication_table:
            SessionTable.__authentication_table[id_user] = { token_id : exp_date }
            return 1
        else:
            session_tokens = SessionTable.__authentication_table[id_user]
            if len(session_tokens) < SessionTable.__MAX_SESSIONS_PER_USER:
                session_tokens[token_id] = exp_date
                return 1
            else:
                now = datetime.datetime.now()
                for tok in list(session_tokens):
                    if session_tokens[tok] < now:
                        del session_tokens[tok]
                if len(session_tokens) < SessionTable.__MAX_SESSIONS_PER_USER:
                    session_tokens[token_id] = exp_date
                    return 1
        # Returns 1 if the token was added, 0 otherwise
        return 0
    
    @staticmethod
    def remove(id_user,token_id):
        try:
            session_tokens = SessionTable.__authentication_table[id_user]

            del session_tokens[token_id]
            return 1
        except KeyError:    
            return 0

    @staticmethod
    def contains(id_user,token_id):
        try:
            SessionTable.__authentication_table[id_user][token_id]
            return 1
        except:
            return 0

    @staticmethod
    def size(id_user):
        try:
            return len(SessionTable.__authentication_table[id_user])
        except:
            return 0



class LoginAPI(Resource):
    def post(self):
        auth = request.authorization

        #generate_password_hash("epah_mas_que_chatice")

        if not auth or not auth.username or not auth.password:
            return make_response(unauthorized("Fields not specified"), 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})

        user = User.get_user(auth.username)

        if not user:
            return make_response(error_code(ErrorCode.USER_DOESNT_EXISTS) , 401 , {'WWW-Authenticate' : 'Basic realm="Login required!"'})

        if user.check_password(auth.password):
            token = create_access_token(identity=str(user.id_user), expires_delta=datetime.timedelta(days=1))
            
            decoded_token = decode_token(token)
            added = SessionTable.add(user.id_user,decoded_token['jti'],datetime.datetime.fromtimestamp( decoded_token['exp'] )) # Old code #token = jwt.encode({'id_user' : auth.username, 'exp' : datetime.datetime.utcnow() + datetime.timedelta(minutes=5)}, app.config['TOKEN_GEN_KEY'])
            print("Sessions:",SessionTable.size(user.id_user))

            if added:    
                return make_response(jsonify({'token' : token }),200)
            else:
                return make_response(unauthorized("Unable to login"), 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})   

        return make_response(error_code(ErrorCode.WRONG_PASSWORD), 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})


class LogoutAPI(Resource):
    @auth_required
    def post(self):
        decoded_token = get_raw_jwt()
        token_id = decoded_token['jti']
        sender_id_user = get_jwt_identity() # or #decoded_token['identity']
        
        torf = SessionTable.remove(sender_id_user,token_id)
        print("Removed? ",torf)
        print("Sessions:",SessionTable.size(sender_id_user))
        if torf:
            return success("Logged out successfully") , 200
        else:
            return error_message("Could not loggout") , 500