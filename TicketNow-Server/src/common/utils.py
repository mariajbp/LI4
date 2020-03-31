from flask import request, jsonify
import jwt
from functools import wraps
from model.users import User
from common.app_init import app

from enum import Enum , unique

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

    
    
def auth_required(f):
    @wraps(f)
    def api_method(*args, **kwargs):
        token = request.args.get('token')

        print(token)

        if not token:
            return jsonify({'message' : 'Token is missing!'}), 403

        try: 
            print("Debug 2")
            data = jwt.decode(token, app.config['TOKEN_GEN_KEY'])
            current_user = User.get_user(data['id_user'])
        except:
            return jsonify({'message' : 'Token is invalid!'}), 403

        print("Debug 3")
        return f(current_user, *args, **kwargs)

    return api_method