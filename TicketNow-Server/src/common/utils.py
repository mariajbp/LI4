from flask import request, jsonify
import jwt
from functools import wraps
from model.users import User
from common.app_init import app
from flask_jwt_extended import jwt_required , get_jwt_claims
from common.responses import forbidden
from common.error import ErrorCode , ErrorCodeException

from enum import Enum , unique
import re


VALID_ID_USER_REGEX = '[a-zA-Z][a-zA-Z0-9]{1,9}'
VALID_NAME_REGEX = "^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$"
VALID_EMAIL_REGEX = '^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$'
VALID_PASSWORD_REGEX = '^[a-zA-Z]\w{3,61}$'#'^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$'
VALID_ID_TICKET_REGEX = '^[0-9a-fA-F]{64}$'


def params_validation(rgxs,params):
    for param,rgx in zip(params,rgxs):
        if re.match(rgx,param) == None:
            raise ErrorCodeException(ErrorCode.INVALID_PARAMETER)


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