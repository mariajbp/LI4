from common.app_init import app , api , db
from werkzeug.security import check_password_hash, generate_password_hash


class User(db.Model):
    __tablename__ = "User"
    
    id_user = db.Column(db.String(10), primary_key=True)
    email = db.Column(db.String(320), nullable=False)
    password_hash = db.Column(db.String(200), nullable=False)
    name = db.Column(db.String(100), nullable=True)
    permissions = db.Column(db.Integer(), nullable=False, default=True)

    def __init__(self,id_user,email,password,name=None,permissions=None):
        self.id_user = id_user
        self.email = email
        self.password_hash = generate_password_hash(password)
        self.name = name
        self.permissions = permissions

    @staticmethod
    def get_user(id_user):
        return User.query.filter_by(id_user=id_user).first()

    @staticmethod
    def get_all():
        return User.query.all()

    @staticmethod
    def add_user(user):
        from common.utils import ErrorCode, ErrorCodeException

        if User.get_user(user.id_user):
            raise ErrorCodeException(ErrorCode.USER_EXISTS)
        db.session.add(user)
        db.session.commit()

    @staticmethod
    def delete(id_user):
        from common.utils import ErrorCode, ErrorCodeException

        u = User.query.filter(User.id_user==id_user)
        
        if u.delete() == 1:
            db.session.commit()
        else:
            raise ErrorCodeException(ErrorCode.USER_DOESNT_EXISTS)



    def to_json(self):
        return { 
            "id_user" : self.id_user,
            "email" : self.email,
            "name" : self.name
        }

    def check_password(self,password):
        return check_password_hash(self.password_hash,password)

    def check_permission(self,const_permition):
        return self.permissions & const_permition

    def __str__(self):
        return  self.id_user