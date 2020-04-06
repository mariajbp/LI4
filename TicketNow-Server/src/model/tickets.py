from common.app_init import db

class Ticket(db.Model):
    __tablename__ = "Ticket"
    
    id_ticket = db.Column(db.String(16), primary_key=True)
    id_user = db.Column(db.String(10), nullable=False)
    type = db.Column(db.Integer(), nullable=False)
    used = db.Column(db.Boolean(), nullable=False,default=True)

    def __init__(self,id_ticket,id_user,type,used=None):
        self.id_ticket = id_ticket
        self.id_user = id_user
        self.type = type
        self.used = used

    @staticmethod
    def get_ticket(id_ticket):
        return Ticket.query.filter_by(id_ticket=id_ticket).first()

    @staticmethod
    def get_all():
        return Ticket.query.all()

    @staticmethod
    def add_ticket(ticket):
        from common.utils import ErrorCode, ErrorCodeException
        
        if Ticket.get_user(ticket.id_user):
            raise ErrorCodeException(ErrorCode.USER_EXISTS)

        db.session.add(ticket)
        db.session.commit()

    def to_json(self):
        return { 
            "id_ticket" : self.id_ticket,
            "id_user" : self.id_user,
            "type" : self.type,
            "used" : self.used
        }