from flask_restful import Resource , reqparse
from model.tickets import Ticket
from flask_jwt_extended import jwt_required , get_jwt_identity
from common.utils import ErrorCodeException , admin_required

class TicketAPI(Resource):
    parser = reqparse.RequestParser()
    parser.add_argument('id_ticket', type=str, required=False, help='Ticket Identifier')

    @admin_required
    def get(self):
        args = TicketAPI.parser.parse_args()
        id_ticket = args['id_ticket']

        return { "tickets" : [ t.to_json() for t in Ticket.get_all() ] } if not id_ticket  \
            else { "ticket" : Ticket.get_ticket(id_ticket).to_json() }