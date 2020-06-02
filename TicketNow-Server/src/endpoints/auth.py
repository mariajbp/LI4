import datetime
import jwt
from flask_jwt_extended import create_access_token , decode_token , get_raw_jwt , get_jwt_identity
from flask_restful import Resource, reqparse
from flask import request , jsonify , make_response
from model.users import User
from common.app_init import app
from common.utils import auth_required, Permissions
from common.error import ErrorCode, ErrorCodeException
from common.responses import error_code , unauthorized , success , error_message
from model.session_table import SessionTable


class LoginAPI(Resource):
    def post(self):
        auth = request.authorization

        #generate_password_hash("epah_mas_que_chatice")

        if not auth or not auth.username or not auth.password:
            return make_response(unauthorized("Fields not specified"), 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})

        try:
            user = User.get_user(auth.username)
        except ErrorCodeException as ec:
            return error_code(ec), 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'}

        #if not user:
        #    return make_response(error_code(ErrorCode.USER_DOESNT_EXISTS) , 401 , {'WWW-Authenticate' : 'Basic realm="Login required!"'})

        if user.check_password(auth.password):
            token = create_access_token(identity=str(user.id_user), user_claims={ "permissions" : user.permissions }, expires_delta=datetime.timedelta(days=1))
            
            decoded_token = decode_token(token)
            added = SessionTable.add(user.id_user,decoded_token['jti'],datetime.datetime.fromtimestamp( decoded_token['exp'] )) # Old code #token = jwt.encode({'id_user' : auth.username, 'exp' : datetime.datetime.utcnow() + datetime.timedelta(minutes=5)}, app.config['TOKEN_GEN_KEY'])
            #print("Sessions:",SessionTable.size(user.id_user))

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
        sender_id_user = get_jwt_identity() # or # decoded_token['identity']
        
        torf = SessionTable.remove(sender_id_user,token_id)
        #print("Removed? ",torf)
        #print("Sessions:",SessionTable.size(sender_id_user))
        if torf:
            return success("Logged out successfully") , 200
        else:
            return error_message("Could not loggout") , 500


class RegisterAPI(Resource):
    parser_post = reqparse.RequestParser()
    parser_post.add_argument('id_user', type=str, required=True, help='User ID')
    parser_post.add_argument('name', type=str, required=True, help='Your name')
    parser_post.add_argument('password', type=str, required=True, help='Password')
    parser_post.add_argument('email', type=str, required=True, help='Email')
    

    def post(self):
        args = RegisterAPI.parser_post.parse_args()

        id_user = args['id_user']
        name = args['name']
        password = args['password']
        email = args['email']

        if not (id_user and name and password and email):
            return error_message("Insuficient arguments") , 400
        # Input validation process here ...

        try:
            User.add_user(User(id_user,email,password,name,Permissions.USER))
            return success() , 200
        except ErrorCodeException as ec:
            return error_code(ec) , 500