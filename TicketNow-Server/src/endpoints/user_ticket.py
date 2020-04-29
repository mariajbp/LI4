from flask_restful import Resource , reqparse
from flask import request
from flask_jwt_extended import get_jwt_identity
from model.tickets import Ticket
from model.users import User
from common.utils import user_required , Permissions
from common.error import ErrorCodeException
from common.responses import unauthorized


class UserTicketAPI(Resource):
    parser = reqparse.RequestParser()
    parser.add_argument('id_ticket', type=str, required=False, help='Ticket Identifier')

    @user_required
    def get(self,id_user):
        args = UserTicketAPI.parser.parse_args()
        
        target_id_ticket = args['id_ticket']
        
        sender_user = User.get_user(get_jwt_identity())

        if sender_user.id_user != id_user and not sender_user.check_permission(Permissions.ADMIN):
            return {'error' : 'Unauthorized!'}, 401

        # Checks if id_ticket has been specified
        if target_id_ticket != None:
            try:
                t = Ticket.get_ticket(target_id_ticket)
                if t.id_user == id_user:
                    return { "ticket" : Ticket.get_ticket(target_id_ticket).to_json() }
            except ErrorCodeException as ec:
                return error_code(ec)

        else:
            return { "owned_tickets" : [ t.to_json() for t in Ticket.get_not_used(id_user=id_user) ] }