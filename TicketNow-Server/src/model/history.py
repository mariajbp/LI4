from common.app_init import db
from common.error import ErrorCode, ErrorCodeException
import datetime

class History(db.Model):
    __tablename__ = "History"
    used_datetime = db.Column(db.DateTime(), default=datetime.datetime.utcnow)
    id_ticket = db.Column(db.String(16), primary_key=True)
    id_user = db.Column(db.String(10), nullable=False)
    

    def __init__(self,id_ticket,id_user,used_datetime=None):
        self.used_datetime = used_datetime
        self.id_ticket = id_ticket
        self.id_user = id_user

    @staticmethod
    def get_entry(id_ticket):
        e = History.query.filter_by(id_ticket=id_ticket).first()
        if e == None:
            raise ErrorCodeException(ErrorCode.HISTORY_ENTRY_DOESNT_EXISTS)
        return e

    @staticmethod
    def get_between(begin,end):
        return History.query.filter(History.used_datetime >= begin , History.used_datetime <= end)

    @staticmethod
    def add_entry(entry):
        try:
            History.get_entry(entry.id_ticket)
            raise ErrorCodeException(ErrorCode.HISTORY_ENTRY_EXISTS)
        except ErrorCodeException as ec:
            db.session.add(entry)
            db.session.commit() 

        

    @staticmethod
    def delete(used_date,id_ticket):
        t = HistoryHistory.query.filter_by(id_ticket=id_ticket)
        
        if t.delete() == 1:
            db.session.commit()
        else:
            raise ErrorCodeException(ErrorCode.TICKETTYPE_DOESNT_EXISTS)



    def to_json(self):
        return { 
            "used_datetime" : self.used_datetime,
            "id_ticket" : self.id_ticket,
            "id_user" : self.id_user
        }