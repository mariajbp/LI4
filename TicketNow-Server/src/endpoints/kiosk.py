from flask_restful import Resource , reqparse
from flask import request
from model.tickets import Ticket
from model.transaction import Transaction
from model.ticket_types import TicketType
#from model.users import User
from flask_jwt_extended import get_jwt_identity
from common.utils import user_required
from common.error import ErrorCodeException
from common.responses import success , error_code , error_message
import requests
import datetime


###################### PAYPAL AUXILIAR PROCEDURES ###################### 

def approve_link(order_id):
    return 'https://www.sandbox.paypal.com/checkoutnow?token={}'.format(order_id)

def status_link(order_id):
    return 'https://api.sandbox.paypal.com/v2/checkout/orders/{}'.format(order_id)

def capture_link(order_id):
    return 'https://api.sandbox.paypal.com/v2/checkout/orders/{}/capture'.format(order_id)

def gen_oauth2_token(username, password):
    url = 'https://api.sandbox.paypal.com/v1/oauth2/token?grant_type=client_credentials'
    response = requests.post(url, auth = (username, password))
    return response.json()['access_token'] if response.status_code == 200 else None

def gen_order(auth_token, price):
    url = 'https://api.sandbox.paypal.com/v2/checkout/orders'
    header = {
        'Authorization': 'Bearer ' + auth_token,
        'Content-Type': 'application/json'
        }
    data = {
        "intent": "CAPTURE",
        "purchase_units": [
            {
                "amount": {
                    "currency_code": "EUR",
                    "value": str(price)
                }
            }
        ]
    }
    response = requests.post(url, headers=header, json=data)
    
    if response.status_code == 201:
        return response.json()['id']
    else:
        return None

def capture_order(auth_token, order_id):
    header = {
        'Authorization': 'Bearer ' + auth_token,
        'Content-Type': 'application/json'
        }
    response = requests.post(capture_link(order_id), headers=header)
    return response.status_code == 201


########################################################################

class KioskAPI(Resource):
    __MAX_TICKET_AMOUNT = 100

    __OAUTH_TOKEN = gen_oauth2_token('AVuNdJP1b1ysHIWfqsbVvBOs57ZSC3pO7cUXjmx6X6OpCp6WWS89KdxRORun9Vp1gHzeLeqsQT5mtazx','EHOYdoXBy_acDWOpKmo0Iw3XdvG0PUJ7pVBB51D3UnwLg8umt_7MLO6__cspUUPyZdwVMxyUT6wBRbWS')#'A21AAFT7PJ5p88zfNcpn7-Zzz4EpxOz9xDK5Ajrl998eOpJDMZUXyRnNS0TwR-oahrZl13mOFQ2K23YQRsAk4uCRPCxbZWU-Q'
    awaiting_transactions = {}

    #parser_get = reqparse.RequestParser()
    #parser_get.add_argument('ticket_type', type=int, required=True, help='Ticket Type Identifier')

    parser_post = reqparse.RequestParser()
    parser_post.add_argument('ticket_type', type=int, required=True, help='Ticket Type Identifier')
    parser_post.add_argument('amount', type=int, required=True, help='Amount of tickets to buy')

    parser_patch = reqparse.RequestParser()
    parser_patch.add_argument('id_transaction', type=str, required=True, help='Pending transaction Identifier')

    #@user_required
    #def get(self):
    #    args = KioskAPI.parser_post.parse_args()
    #    
    #    ticket_type = args['ticket_type']
#
    #    try:
    #        tt = TicketType.get_type(ticket_type)
    #    
    #    
    #    except ErrorCodeException as ec:
    #        return error_code(ec) , 500



    @user_required
    def post(self):
        args = KioskAPI.parser_post.parse_args()
        target_id_user = get_jwt_identity()

        amount = args['amount']
        ticket_type = args['ticket_type']

        from binascii import hexlify

        price = 0.0
        try:
            price = TicketType.get_type(ticket_type).price
        except ErrorCodeException as ec:
            return error_code(ec) , 500
        
        if amount > self.__MAX_TICKET_AMOUNT or amount <= 0:
            return error_message("Invalid amount MAX = " + str(__MAX_TICKET_AMOUNT)) , 500
        
        order_id = gen_order(KioskAPI.__OAUTH_TOKEN, price*amount)
        print(order_id)
        if order_id == None:
            return error_message("Something unexpected ocurred") , 500
        
        KioskAPI.awaiting_transactions[order_id] = {
            "amount" : amount,
            "price" : price,
            "ticket_type" : ticket_type,
            "id_user" : target_id_user
        }
        
        
        return { 
            "transaction_status" : "Payment",
            "total_price" : price*amount,
            "id_transaction": order_id,
            "paypal_link" : approve_link(order_id)
            } , 201


    @user_required
    def patch(self):
        args = KioskAPI.parser_patch.parse_args()
        target_id_user = get_jwt_identity()

        id_transaction = args['id_transaction']

        if not id_transaction in KioskAPI.awaiting_transactions:
            return error_message("Invalid transaction identifier"), 500
        

        context = KioskAPI.awaiting_transactions[id_transaction]

        if context['id_user'] != target_id_user:
            return error_message("Forbidden") , 401
            
        if not capture_order(KioskAPI.__OAUTH_TOKEN,id_transaction):
            return { 
            "transaction_status" : "Payment"
            } , 200

        bought = []
        from binascii import hexlify
        try:
            total_price = context['price']*context['amount']
            dtt_now = datetime.datetime.now()
            for i in range(context['amount']):
                tckt = Ticket(target_id_user, context['ticket_type'])
                #print(str(tckt.id_ticket))

                bought.append(hexlify(tckt.id_ticket).decode('ascii'))
                Ticket.add_ticket(tckt)
                Transaction.add_transaction(Transaction(id_transaction, context['id_user'], tckt.id_ticket, total_price, dtt_now))
        except ErrorCodeException as ec:
            return error_code(ec) , 500
        except Exception as e:
            print(e)
            return error_message("Something unexpected occurred") , 500

        return { 
            "transaction_status" : "Success",
            "ticket_ids" : bought
            } , 201


#########################################################################

#        args = KioskAPI.parser_post.parse_args()
#
#        target_id_user = get_jwt_identity()
#
#        amount = args['amount']
#        ticket_type = args['ticket_type']
#        
#        from binascii import hexlify
#        bought = []
#
#        price = 0.0
#        try:
#            price = TicketType.get_type(ticket_type).price
#        except ErrorCodeException as ec:
#            return error_code(ec) , 500
#        
#        if amount > self.__MAX_TICKET_AMOUNT or amount <= 0:
#            return error_message("Invalid amount MAX = " + str(__MAX_TICKET_AMOUNT)) , 500
#        
#        try:
#            for i in range(amount):
#                tckt = Ticket(target_id_user,ticket_type)
#                #print(str(tckt.id_ticket))
#
#                bought.append(hexlify(tckt.id_ticket).decode('ascii'))
#                Ticket.add_ticket(tckt)
#        except ErrorCodeException as ec:
#            return error_code(ec) , 500
#        except :
#            return error_message("Something unexpected occurred") , 500
#        
#        
#        return { 
#            "transaction_status" : "Success",
#            "price" : price*amount,
#            "ticket_ids" : bought
#            } , 200