from flask_restful import Resource , reqparse
from flask import request
from model.history import History
from flask_jwt_extended import get_jwt_identity
from common.utils import auth_required
from common.error import ErrorCodeException
from common.responses import error_code , unauthorized

#class HistoryAPI(Resource):
#    parser_get = reqparse.RequestParser()
#    parser_get.add_argument('id_user', type=str, required=False, help='User Identifier')




class UserHistoryAPI(Resource):

    @user_required
    def get(self,id_user):

        sender_id_user = get_jwt_identity()
        if sender_id_user != id_user:
            return unauthorized("Forbiden!"), 403
        try:
            user_hist = History.get_all(id_user)
            return { "history" : [ he.to_json() for he in user_hist ] if user_hist else [] } , 200
        except ErrorCodeException as ec:
            return error_code(ec) , 500

    # TODO: Add the rest of the methods POST PUT DELETE