from common.app_init import db
from common.error import ErrorCode, ErrorCodeException
import datetime
from binascii import hexlify , unhexlify

class History(db.Model):
    __tablename__ = "History"
    used_datetime = db.Column(db.DateTime(), default=datetime.datetime.utcnow)
    id_ticket = db.Column(db.BINARY(32), primary_key=True)
    id_user = db.Column(db.String(10), nullable=False)
    

    def __init__(self,id_ticket,id_user,used_datetime=None):
        self.used_datetime = used_datetime
        self.id_ticket = hexlify(id_ticket).decode('ascii')
        self.id_user = id_user

    @staticmethod
    def get_entry(id_ticket):
        e = History.query.filter_by(id_ticket=unhexlify(id_ticket)).first()
        if e == None:
            raise ErrorCodeException(ErrorCode.HISTORY_ENTRY_DOESNT_EXISTS)
        return e

    @staticmethod
    def get_all(id_user=None):
        if id_user == None:
            return History.query.all()
        else:
            return History.query.filter_by(id_user=id_user)

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
    def delete(id_ticket):
        t = History.query.filter_by(id_ticket=unhexlify(id_ticket))
        
        if t.delete() == 1:
            db.session.commit()
        else:
            raise ErrorCodeException(ErrorCode.HISTORY_ENTRY_DOESNT_EXISTS)



    def to_json(self):
        return { 
            "used_datetime" : str(self.used_datetime),
            "id_ticket" : hexlify(self.id_ticket).decode('ascii'),
            "id_user" : self.id_user
        }