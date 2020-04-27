from flask import request, jsonify, make_response
from flask_restful import Resource
from werkzeug.security import generate_password_hash, check_password_hash
import jwt
import datetime
from functools import wraps

from common.app_init import app , api , db
from common.utils import auth_required
from endpoints.login import LoginAPI , LogoutAPI , SessionTable
from endpoints.user import UserAPI
from endpoints.ticket import TicketAPI
from endpoints.user_ticket import UserTicketAPI
from endpoints.ticket_type import TicketTypeAPI
from endpoints.validator import ValidatorAPI

# Used for populate
from model.users import User
from model.ticket_types import TicketType
from model.tickets import Ticket

#########################################################################    

base_endpoint = app.config['BASE_ENDPOINT']

api.add_resource(UserAPI, base_endpoint + '/user')
api.add_resource(TicketAPI, base_endpoint + '/ticket')
api.add_resource(LoginAPI, base_endpoint + '/login')
api.add_resource(LogoutAPI, base_endpoint + '/logout')
api.add_resource(UserTicketAPI, base_endpoint + '/user/<id_user>/tickets')
api.add_resource(TicketTypeAPI, base_endpoint + '/ticket_type')
api.add_resource(ValidatorAPI, base_endpoint + '/validator')

#########################################################################

def populate():

    # Insert some users
    try:
        User.add_user(User("a12345","a12345@alunos.uminho.pt","epah_mas_que_chatice","António Barosa"))
    except:
        pass
    try:
        User.add_user(User("a11111","a11111@alunos.uminho.pt","epah_mas_que_chatice","Adelino Costa"))
    except:
        pass
    try:
        User.add_user(User("a22222","a22222@alunos.uminho.pt","epah_mas_que_chatice","Rosa Matilde"))
    except:
        pass
    try:
        User.add_user(User("a33333","a33333@alunos.uminho.pt","epah_mas_que_chatice","Fernando Magalhães"))
    except:
        pass 
        
    # Insert some ticket types
    try:
        TicketType.add_type(TicketType(1,1.8,"simple"))
    except:
        pass
    try:
        TicketType.add_type(TicketType(2,2.5,"complete"))
    except:
        pass
    # Insert some tickets
    try:
        Ticket.add_ticket(Ticket("aaaaaaaaaaaaaaaa","a12345",1))
    except:
        pass
    try:
        Ticket.add_ticket(Ticket("aaaaaaaaaaaaaaab","a12345",1))
    except:
        pass
    try:
        Ticket.add_ticket(Ticket("aaaaaaaaaaaaaaac","a12345",1))
    except:
        pass
    try:
        Ticket.add_ticket(Ticket("bbbbbbbbbbbbbbbb","a12345",2))
    except:
        pass

    try:
        from model.tickets import set_as_used
        set_as_used("aaaaaaaaaaaaaaab","a12345")
    except:
        pass


           

populate()

if __name__ == '__main__':
    app.run(host='0.0.0.0',debug=True)

#   app.run(host='0.0.0.0',debug=True,ssl_context='adhoc')

#########################################################################

## Code relative to google calendar api

#from __future__ import print_function
#from httplib2 import Http
#from googleapiclient.discovery import build
#from oauth2client import file, client, tools
#
#store = file.Storage('client_secret.json')
#creds = store.get()
#if not creds or creds.invalid:
#    flow = client.flow_from_clientsecrets(
#            'client_secret.json',
#            'https://www.googleapis.com/auth/calendar.readonly')
#    creds = tools.run_flow(flow, store)
#service = build('calendar', 'v3', http=creds.authorize(Http()))
#
#def get_calendar_ids():
##return all added extenal calendars in a dict
##with calendar name as key and calendar id as value
#    page_token = None
#    calendar_ids = {}
#    while True:
#        calendar_list = service.calendarList().list(pageToken=page_token).execute()
#        for calendar_list_entry in calendar_list['items']:
#            if '@group.calendar.google.com' in calendar_list_entry['id']:
#                key = calendar_list_entry['summary']
#                if 'summaryOverride' in calendar_list_entry:
#                   key = calendar_list_entry['summaryOverride']
#                calendar_ids[key] = calendar_list_entry['id']
#        page_token = calendar_list.get('nextPageToken')
#        if not page_token:
#            break
#
#    return calendar_ids
#
#now = datetime.datetime.utcnow().isoformat() + 'Z'
#events_result = service.events().list(
#            calendarId = get_calendar_ids()["A"],
#            timeMin = now,
#            maxResults = 3,
#            singleEvents = True,
#            orderBy = 'startTime').execute()
#
#
#for event in events_result.get('items', []):
#    arrayDate = event['start'].get('dateTime', event['start'].get('date')).split('T')[0].split('-')
#            
#    print(arrayDate[2] + '-' + arrayDate[1] + '-' + arrayDate[0])
#    print()
#    print(event['summary'])