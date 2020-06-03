from flask_restful import Resource , reqparse
from flask import request
from model.users import User
from flask_jwt_extended import jwt_required , get_jwt_identity , get_raw_jwt
from common.utils import admin_required , user_required , auth_required , Permissions
from common.error import ErrorCodeException
from common.responses import success , error_code , forbidden , error_message


class UserAPI(Resource):
    parser_del = reqparse.RequestParser()
    parser_del.add_argument('id_user', type=str, required=False, help='User Identifier')

    parser_put = reqparse.RequestParser()
    parser_put.add_argument('id_user', type=str, required=True, help='Old Password')
    parser_put.add_argument('old_password', type=str, required=True, help='Old Password')
    parser_put.add_argument('new_password', type=str, required=True, help='New Password')
    
    
    @admin_required
    def get(self):
        sender_user = User.get_user(get_jwt_identity())

        # If id_user hasn't been specified check permissions for listing all users
        if not sender_user.check_permission(Permissions.ADMIN):
            return forbidden() , 401
        return { "users" : [ u.to_json() for u in User.get_all() ] }


    @admin_required
    def post(self):
        body = request.json
        if not body:
            return { "error" : "No json body found on request" }
        
        id_user = body['id_user']
        email = body['email']
        password = body['password']
        name = body['name']

        ################## Input Validation ##################
        from common.utils import params_validation, VALID_ID_USER_REGEX , VALID_NAME_REGEX , VALID_PASSWORD_REGEX , VALID_EMAIL_REGEX

        params = [ id_user             , name             , password             , email             ]
        rgxs   = [ VALID_ID_USER_REGEX , VALID_NAME_REGEX , VALID_PASSWORD_REGEX , VALID_EMAIL_REGEX ]
        
        try:
            params_validation(rgxs,params)
        except ErrorCodeException as ec:
            return error_code(ec) , 400
        ######################################################

        try:
            User.add_user(User(body['id_user'],body['email'],body['password'],body['name'],int(body['permissions'])))
            return success()
        except ErrorCodeException as ec:
            return error_code(ec)


    @admin_required
    def delete(self):
        args = UserAPI.parser_del.parse_args()
        id_user = args['id_user']

        if not id_user:
            return { "error" : "Argument required" }

        try:
            User.delete(id_user)
            return success()
        except ErrorCodeException as ec:
            return error_code(ec)
        

    @auth_required
    def put(self):
        sender_id_user = get_jwt_identity()
        args = UserAPI.parser_put.parse_args()

        target_id_user = args['id_user']
        old_password = args['old_password']
        new_password = args['new_password']
    
        if old_password != None and new_password != None :
            try:
                u = User.get_user(target_id_user)
                tmp = User.get_user(sender_id_user)
                if sender_id_user != target_id_user and not tmp.check_permission(Permissions.ADMIN):
                    return forbidden() , 403
                else:
                    if u.check_password(old_password):
                        u.set_password(new_password)
                        return success()
                    else:
                        return error_message("Incorrect old password!") , 500
            except ErrorCodeException as ec:
                return error_code(ec) , 500
        else:
            return error_message("Argument required") , 401


""" {
	"id_user" : "a11111",
	"email" : "a11111@alunos.uminho.pt",
	"password" : "password segura" ,
	"name" : "Jubilado HEHE"
} """

class UserInfoAPI(Resource):
    @user_required
    def get(self,id_user):
        sender_user = User.get_user(get_jwt_identity())

        # Check permissions for listing other users rather than himself
        if (not sender_user.check_permission(Permissions.ADMIN)) and (not id_user == sender_user.id_user):
            return forbidden() , 403

        try:
            return { "user" : User.get_user(id_user).to_json() }
        except ErrorCodeException as ec:
            return error_code(ec)
        # Also check if argument user exists