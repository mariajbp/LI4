from common.app_init import db
from common.error import ErrorCode, ErrorCodeException
from binascii import hexlify , unhexlify

class Ticket(db.Model):
    __tablename__ = "Ticket"
    
    id_ticket = db.Column(db.BINARY(32), primary_key=True)
    id_user = db.Column(db.String(10), nullable=False)
    type = db.Column(db.Integer(), nullable=False)
    used = db.Column(db.Boolean(), nullable=False,default=0)

    def __init__(self,id_user,type,id_ticket=None,used=None):
        self.id_ticket = self.gen_id(id_user,type) if id_ticket == None else id_ticket 
        self.id_user = id_user
        self.type = type
        self.used = used

    @staticmethod
    def get_ticket(id_ticket):
        t = Ticket.query.filter_by(id_ticket=unhexlify(id_ticket)).first()
        if t == None:
            raise ErrorCodeException(ErrorCode.TICKET_DOESNT_EXISTS)
        return t

    @staticmethod
    def get_all():
        return Ticket.query.all()

    @staticmethod
    def get_not_used(id_user = None):
        if id_user == None:
            return Ticket.query.filter_by(used=0)
        else:
            return Ticket.query.filter_by(used=0,id_user=id_user)

    @staticmethod
    def add_ticket(ticket):
        try:
            Ticket.get_ticket(hexlify(ticket.id_ticket).decode('ascii'))
        except ErrorCodeException:
            db.session.add(ticket)
            db.session.commit()
            return
        raise ErrorCodeException(ErrorCode.TICKET_EXISTS)


    @staticmethod
    def delete(id_ticket):
        
        t = Ticket.query.filter(Ticket.id_ticket==unhexlify(id_ticket))
        
        if t.delete() == 1:
            db.session.commit()
        else:
            raise ErrorCodeException(ErrorCode.TICKET_DOESNT_EXISTS)

    @staticmethod
    def gen_id(id_user,type,salt=''):
        from datetime import datetime
        from hashlib import sha256
        res = sha256((id_user + str(datetime.now()) + salt).encode('ascii')).digest()
        return res


    def set_used(self):
        self.used = True
        db.session.commit()

    def to_json(self):
        return { 
            "id_ticket" : hexlify(self.id_ticket).decode('ascii'),
            "id_user" : self.id_user,
            "type" : self.type,
            "used" : self.used
        }

from model.history import History
from model.users import User

def set_as_used(id_ticket,id_user):
    
    t = Ticket.get_ticket(id_ticket)
    if t == None:
        raise ErrorCodeException(ErrorCode.TICKET_DOESNT_EXISTS)

    if User.get_user(id_user) == None:
        raise ErrorCodeException(ErrorCode.USER_DOESNT_EXISTS)
    
    if t.used == True:
        raise ErrorCodeException(ErrorCode.TICKET_ALREADY_USED)
    t.set_used()
    
    History.add_entry(History(id_ticket=id_ticket,id_user=id_user))