from flask_restful import Resource , reqparse
from flask import request
from model.users import User
from flask_jwt_extended import jwt_required , get_jwt_identity
from common.utils import admin_required , auth_required , Permissions
from common.error import ErrorCodeException
from common.responses import success , error_code


class UserAPI(Resource):
    parser = reqparse.RequestParser()
    parser.add_argument('id_user', type=str, required=False, help='User Identifier')
    
    parser.add_argument('old_password', type=str, required=False, help='Old Password')
    parser.add_argument('new_password', type=str, required=False, help='New Password')
    
    
    @auth_required
    def get(self):
        args = UserAPI.parser.parse_args()
        
        target_id_user = args['id_user']
        
        sender_user = User.get_user(get_jwt_identity())

        # Checks if id_user has been specified
        if target_id_user:

            # Check permissions for listing other users rather than himself
            if (not sender_user.check_permission(Permissions.ADMIN)) and (not target_id_user == sender_user.id_user):
                return { "error" : "Unauthorized!" } , 401

            try:
                return { "user" : User.get_user(target_id_user).to_json() }
            except ErrorCodeException as ec:
                return error_code(ec)
            # Also check if argument user exists

        else:
            # If id_user hasn't been specified check permissions for listing all users
            if not sender_user.check_permission(Permissions.ADMIN):
                return { "error" : "Unauthorized!" } , 401
            return { "users" : [ u.to_json() for u in User.get_all() ] }


    @admin_required
    def post(self):
        body = request.json
        if not body:
            return { "error" : "No json body found on request" }
        
        try:
            User.add_user(User(body['id_user'],body['email'],body['password'],body['name']))
            return success()
        except ErrorCodeException as ec:
            return error_code(ec)


    @admin_required
    def delete(self):
        args = UserAPI.parser.parse_args()
        id_user = args['id_user']

        print(id_user)

        if not id_user:
            print("1")
            return { "error" : "Argument required" }

        try:
            User.delete(id_user)
            print("2")
            return success()
        except ErrorCodeException as ec:
            print("3")
            return error_code(ec)
        

    @admin_required
    def put(self):
        id_user = get_jwt_identity()
        args = UserAPI.parser.parse_args()
        old_password = args['old_password']
        new_password = args['new_password']

        if old_password and new_password : 
            print(id_user)
            return success()
        else:
            return { "error" : "Argument required" }

        
""" {
	"id_user" : "a11111",
	"email" : "a11111@alunos.uminho.pt",
	"password" : "password segura" ,
	"name" : "Jubilado HEHE"
} """