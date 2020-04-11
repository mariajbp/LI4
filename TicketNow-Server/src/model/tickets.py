from common.app_init import db
from common.error import ErrorCode, ErrorCodeException


class Ticket(db.Model):
    __tablename__ = "Ticket"
    
    id_ticket = db.Column(db.String(16), primary_key=True)
    id_user = db.Column(db.String(10), nullable=False)
    type = db.Column(db.Integer(), nullable=False)
    used = db.Column(db.Boolean(), nullable=False,default=0)

    def __init__(self,id_ticket,id_user,type,used=None):
        self.id_ticket = id_ticket
        self.id_user = id_user
        self.type = type
        self.used = used

    @staticmethod
    def get_ticket(id_ticket):
        t = Ticket.query.filter_by(id_ticket=id_ticket).first()
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
            Ticket.get_ticket(ticket.id_ticket)
        except ErrorCodeException:
            db.session.add(ticket)
            db.session.commit()


    @staticmethod
    def delete(id_ticket):
        t = Ticket.query.filter(Ticket.id_ticket==id_ticket)
        
        if t.delete() == 1:
            db.session.commit()
        else:
            raise ErrorCodeException(ErrorCode.TICKET_DOESNT_EXISTS)


    def set_used(self):
        self.used = True
        db.session.commit()

    def to_json(self):
        return { 
            "id_ticket" : self.id_ticket,
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

    #if User.get_user(id_user) == None:
    #    raise ErrorCodeException(ErrorCode.USER_DOESNT_EXISTS)
    
    t.set_used()
    
    History.add_entry(History(id_ticket=id_ticket,id_user=id_user))