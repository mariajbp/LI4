from flask import request, jsonify
import jwt
from functools import wraps
from model.users import User
from common.app_init import app
from flask_jwt_extended import jwt_required , get_jwt_claims
from common.responses import forbidden
#from common.utils import Permissions

from enum import Enum , unique

############## Testing ############## 
class Permissions:
    USER             = 0b00000001
    VALIDATOR        = 0b00000010
    ADMIN            = 0b00000100
    CONTENT_PROVIDER = 0b00001000


def __bit_at(value,index):
    return value & pow(2,index)

#####################################


def __p_required_aux__(f,permission):
    @jwt_required
    @wraps(f)
    def api_method(*args, **kwargs):
        if not (permission & get_jwt_claims()['permissions']):
            return forbidden(), 403
        return f(*args, **kwargs)

    return api_method    

auth_required = jwt_required

user_required = lambda f : __p_required_aux__(f,Permissions.USER | Permissions.ADMIN)

admin_required = lambda f : __p_required_aux__(f,Permissions.ADMIN)

validator_required = lambda f : __p_required_aux__(f,Permissions.VALIDATOR | Permissions.ADMIN)

content_provider_required = lambda f : __p_required_aux__(f,Permissions.CONTENT_PROVIDER | Permissions.ADMIN)