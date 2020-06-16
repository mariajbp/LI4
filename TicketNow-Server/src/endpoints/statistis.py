from flask_restful import Resource , reqparse
from flask import request
from model.history import History
from flask_jwt_extended import get_jwt_identity
from common.utils import auth_required
import datetime
from common.responses import error_message


class StatisticsAPI(Resource):
    parser_get = reqparse.RequestParser()
    parser_get.add_argument('begin', type=str, required=False, help='Inital date')
    parser_get.add_argument('end', type=str, required=False, help='End date')

    @auth_required
    def get(self):
        args = StatisticsAPI.parser_get.parse_args()
        
        #sender_id_user = get_jwt_identity()

        
        today = datetime.date.today() 
        today_minus_week = today - datetime.timedelta(weeks=-1)

        begin = datetime.date.fromisoformat(args['begin']) if args['begin'] != None else today_minus_week
        end   = datetime.date.fromisoformat(args['end']) if args['end'] != None else today
        
        if begin > end:
            return error_message("Invalid dates interval!"), 400
        
        try:
            res = History.db.session.execute('SELECT DATE(used_datetime) as used_date, is_lunch(TIME(used_datetime)) as is_lunch FROM History h WHERE DATE(h.used_datetime) > :begin AND DATE(h.used_datetime) < :end;', {'begin': begin , 'end': end})
            hmm = {}
            for row in res:
                ud = str(row['used_date'])
                if ud not in hmm:
                   hmm[ud] = {
                       'lunch' : 0,
                       'dinner' : 0
                   }

                if row['is_lunch']:
                    hmm[ud]['lunch'] += 1
                else:
                    hmm[ud]['dinner'] += 1
                
                print()
            return {
                "statistics" : hmm
            } , 200
        except Exception as e:
            print(e)
            return error_message("Something unexpected occured!"), 500


class MyStatisticsAPI(Resource):
    parser_get = reqparse.RequestParser()
    parser_get.add_argument('begin', type=str, required=False, help='Inital date')
    parser_get.add_argument('end', type=str, required=False, help='End date')

    @auth_required
    def get(self):
        args = StatisticsAPI.parser_get.parse_args()
        
        sender_id_user = get_jwt_identity()

        
        today = datetime.date.today() 
        today_minus_week = today - datetime.timedelta(weeks=-1)

        begin = datetime.date.fromisoformat(args['begin']) if args['begin'] != None else today_minus_week
        end   = datetime.date.fromisoformat(args['end']) if args['end'] != None else today
        
        if begin > end or :
            return error_message("Invalid dates interval!"), 400
        
        try:
            res = History.db.session.execute('SELECT DATE(used_datetime) as used_date, is_lunch(TIME(used_datetime)) as is_lunch FROM History h WHERE DATE(h.used_datetime) > :begin AND DATE(h.used_datetime) < :end AND id_user = :id_user;', {'begin': begin , 'end': end, 'id_user': sender_id_user})
            hmm = {}
            for row in res:
                ud = str(row['used_date'])
                if ud not in hmm:
                   hmm[ud] = {
                       'lunch' : 0,
                       'dinner' : 0
                   }

                if row['is_lunch']:
                    hmm[ud]['lunch'] += 1
                else:
                    hmm[ud]['dinner'] += 1
                
                print()
            return {
                "statistics" : hmm
            } , 200
        except Exception as e:
            print(e)
            return error_message("Something unexpected occured!"), 500