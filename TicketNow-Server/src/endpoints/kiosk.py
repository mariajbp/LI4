from flask_restful import Resource , reqparse
from flask import request
#from model.tickets import Ticket
#from model.users import User
#from flask_jwt_extended import get_jwt_identity
#from common.utils import auth_required
#from common.error import ErrorCodeException
#from common.responses import success , error_code

#class KioskAPI(Resource):
#    parser = reqparse.RequestParser()
#    parser.add_argument('id_ticket', type=str, required=False, help='Ticket Identifier')
#
#    @auth_required
#    def post(self):