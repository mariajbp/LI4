from common.app_init import db
from common.error import ErrorCode, ErrorCodeException
import datetime


class Meal(db.Model):
    __tablename__ = "Meal"
    date = db.Column(db.Date(), primary_key=True)
    id_location = db.Column(db.Integer(), primary_key=True)
    id_meal_type = db.Column(db.Integer(), primary_key=True)

    soup = db.Column(db.String(32), nullable=True)
    main_dish = db.Column(db.String(32), nullable=True)
    description = db.Column(db.String(64), nullable=True)

    def __init__(self,date,id_location,id_meal_type,soup=None,main_dish=None,description=None):
        self.date = date
        self.id_location = id_location
        self.id_meal_type = id_meal_type
        self.soup = soup
        self.main_dish = main_dish
        self.description = description

    @staticmethod
    def get_meal(date,id_location,id_meal_type):
        m = Meal.query.filter_by(date=date,id_location=id_location,id_meal_type=id_meal_type).first()
        if m == None:
            raise ErrorCodeException(ErrorCode.MEAL_DOESNT_EXISTS)
        return m

    @staticmethod
    def get_all():
        return Meal.query.all()

    @staticmethod
    def get_between(begin,end,id_meal_type=None,id_location=None):
        flt = Meal.query.filter(Meal.date >= begin , Meal.date <= end)
        if id_meal_type != None:
            flt = flt.filter(Meal.id_meal_type==id_meal_type)
        if id_location != None:
            flt = flt.filter(Meal.id_location==id_location)
        return flt

    @staticmethod
    def add_meal(meal):
        try:
            Meal.get_meal(meal.date,meal.id_location,meal.id_meal_type)
        except ErrorCodeException:
            db.session.add(meal)
            db.session.commit()
            return
        raise ErrorCodeException(ErrorCode.MEAL_EXISTS)

    @staticmethod
    def delete(date,id_location,id_meal_type):
        m = Meal.query.filter(Meal.date==date,Meal.id_location==id_location,Meal.id_meal_type==id_meal_type)
        
        if m.delete() == 1:
            db.session.commit()
        else:
            raise ErrorCodeException(ErrorCode.MEAL_DOESNT_EXISTS)

    def to_json(self):
        return { 
            "date" : str(self.date),
            "location" : Location.get_location(self.id_location).name,
            "meal_type" : MealType.get_meal_type(self.id_meal_type).name,
            "soup" : self.soup,
            "main_dish" : self.main_dish,
            "description" : self.description
        }


#################### Mapping data ####################

class MealType(db.Model):
    __tablename__ = "MealType"
    id_meal_type = db.Column(db.Integer(), primary_key=True)
    name = db.Column(db.String(32), nullable=False, unique=True)

    def __init__(self,name):
        self.id_meal_type = None
        self.name = name

    @staticmethod
    def get_meal_type(id_meal_type):
        mt = MealType.query.filter_by(id_meal_type=id_meal_type).first()
        if mt == None:
            raise ErrorCodeException(ErrorCode.MEALTYPE_DOESNT_EXISTS)
        return mt

    @staticmethod
    def get_by_name(name):
        mt = MealType.query.filter_by(name=name).first()
        if mt == None:
            raise ErrorCodeException(ErrorCode.MEALTYPE_DOESNT_EXISTS)
        return mt

    @staticmethod
    def get_all():
        return MealType.query.all()

    @staticmethod
    def add_meal_type(meal_type):
        try:
            MealType.get_meal_type(meal_type.id_meal_type)
        except ErrorCodeException:
            db.session.add(meal_type)
            db.session.commit()
            return
        raise ErrorCodeException(ErrorCode.MEALTYPE_EXISTS)

    @staticmethod
    def delete(id_meal_type):
        mt = MealType.query.filter(Meal.id_meal_type==id_meal_type)
        
        if mt.delete() == 1:
            db.session.commit()
        else:
            raise ErrorCodeException(ErrorCode.MEALTYPE_DOESNT_EXISTS)


class Location(db.Model):
    __tablename__ = "Location"
    id_location = db.Column(db.Integer(), primary_key=True)
    name = db.Column(db.String(32), nullable=False, unique=True)

    def __init__(self,name):
        self.id_location = None
        self.name = name

    @staticmethod
    def get_location(id_location):
        l = Location.query.filter_by(id_location=id_location).first()
        if l == None:
            raise ErrorCodeException(ErrorCode.LOCATION_DOESNT_EXISTS)
        return l

    @staticmethod
    def get_by_name(name):
        l = Location.query.filter_by(name=name).first()
        if l == None:
            raise ErrorCodeException(ErrorCode.LOCATION_DOESNT_EXISTS)
        return l

    @staticmethod
    def get_all():
        return Location.query.all()

    @staticmethod
    def add_location(location):
        try:
            Location.get_location(location.id_location)
        except ErrorCodeException:
            db.session.add(location)
            db.session.commit()
            return
        raise ErrorCodeException(ErrorCode.LOCATION_EXISTS)

    @staticmethod
    def delete(id_location):
        l = Location.query.filter(Location.id_location==id_location)
        
        if l.delete() == 1:
            db.session.commit()
        else:
            raise ErrorCodeException(ErrorCode.LOCATION_DOESNT_EXISTS)