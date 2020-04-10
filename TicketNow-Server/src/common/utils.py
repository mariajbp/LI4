from flask import request, jsonify
import jwt
from functools import wraps
from model.users import User
from common.app_init import app
from flask_jwt_extended import jwt_required, get_jwt_identity
#from common.utils import Permissions

from enum import Enum , unique


class Permissions:
    USER             = 0b00000001
    VALIDATOR        = 0b00000010
    ADMIN            = 0b00000100
    CONTENT_PROVIDER = 0b00001000

"""     @staticmethod
    def has_permission(value,a) -> bool:
        return value & a

    @staticmethod
    def bit_at(value,index):
        return value & pow(2,index) """

def __bit_at(value,index):
    return value & pow(2,index)



@unique
class ErrorCode(Enum):
    UNKNOWN = 0
    USER_EXISTS = 1
    USER_DOESNT_EXISTS = 2
    WRONG_PASSWORD = 3

    def message(self):
        ERROR_MESSAGES = [
            "Unknown error",
            "User already exists",
            "User does not exists",
            "Wrong password"
        ]
        return ERROR_MESSAGES[self.value]

class ErrorCodeException(BaseException):
    def __init__(self,error_code):
        self.error_code = error_code

    def message(self):
        return self.error_code.message()

    

def admin_required(f):
    @jwt_required
    @wraps(f)
    def api_method(*args, **kwargs):
        id_user = get_jwt_identity()
        u = User.get_user(id_user)
        
        if not u.check_permission(Permissions.ADMIN):
            print("")
            return {'error' : 'Unauthorized!'}, 401

        return f(*args, **kwargs)

    return api_method



auth_required = jwt_required
#def auth_required(f):
#    @wraps(f)
#    def api_method(*args, **kwargs):
#        token = request.args.get('token')
#
#        print(token)
#
#        if not token:
#            return jsonify({'message' : 'Token is missing!'}), 401
#
#        try: 
#            data = jwt.decode(token, app.config['TOKEN_GEN_KEY'])
#            current_user = User.get_user(data['id_user'])
#        except:
#            return jsonify({'message' : 'Token is invalid!'}), 401
#
#        return f(current_user, *args, **kwargs)
#
#    return api_method