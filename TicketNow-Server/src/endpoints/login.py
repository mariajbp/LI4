import datetime
import jwt
from flask_jwt_extended import create_access_token
from flask_restful import Resource
from flask import request , jsonify , make_response
from model.users import User
from common.app_init import app
from common.error import ErrorCode
from common.responses import error_code , unauthorized

class LoginAPI(Resource):
    def get(self):
        auth = request.authorization

        #generate_password_hash("epah_mas_que_chatice")

        if not auth or not auth.username or not auth.password:
            return make_response(unauthorized("Unable to login"), 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})

        user = User.get_user(auth.username)

        if not user:
            return make_response(error_code(ErrorCode.USER_DOESNT_EXISTS) , 401 , {'WWW-Authenticate' : 'Basic realm="Login required!"'})

        if user.check_password(auth.password):
            token = create_access_token(identity=str(auth.username), expires_delta=datetime.timedelta(days=7))

            #token = jwt.encode({'id_user' : auth.username, 'exp' : datetime.datetime.utcnow() + datetime.timedelta(minutes=5)}, app.config['TOKEN_GEN_KEY'])
            return make_response(jsonify({'token' : token }),200)
        
        return make_response(error_code(ErrorCode.WRONG_PASSWORD), 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})