import random
import requests
from datetime import datetime, date, timedelta
import string

first_names = ['Adelaide','Adélia','Adelina','Adriana','Ágata','Aida','Albertina','Alexandra','Alice','Amália','Amanda','Amélia','Ana','Anastácia','Andreia','Ângela','Angélica','Angelina','Anita','Augusta','Aurora','Bárbara','Beatriz','Benedita','Berta','Bianca','Bruna','Camila','Cândida','Carina','Carla','Carlota','Carmen','Carmo','Carolina','Catarina','Cátia','Cecília','Celeste','Célia','Cidália','Clara','Clarisse','Cláudia','Conceição','Constança','Cristiana','Cristina','Dalila','Daniela','Débora','Delfina','Denise','Deolinda','Diana','Dina','Dora','Dores','Dulce','Edite','Eduarda','Elisa','Elisabete','Elsa','Ema','Emanuela','Emília','Érica','Esmeralda','Estefânia','Estela','Eugênia','Eva','Fábia','Fabiana','Fabíola','Fátima','Fernanda','Fernando','Filipa','Flávia','Francisca','Gabriela','Gisela','Glória','Graça','Graça','Helena','Helga','Heloísa','Hermínia','Iara','Inês','Irene','Irina','Íris','Isa','Isabel','Isadora','Ivete','Ivone','Jacinta','Jaqueline','Jéssica','Joana','Julia','Juliana','Julieta','Lara','Laura','Leila','Leonor','Letícia','Lídia','Liliana','Lúcia','Luciana','Luísa','Lurdes','Luz','Luzia','Madalena','Mafalda','Manuela','Mara','Márcia','Margarida','Maria','Mariana','Marina','Marisa','Marlene','Marta','Matilde','Micaela','Milena','Mónica','Nádia','Natacha','Natália','Neusa','Nicole','Odélia','Odete','Ofélia','Olga','Olímpia','Olinda','Olívia','Orlanda','Palmira','Patrícia','Paula','Pilar','Priscila','Rafaela','Raquel','Rebeca','Regina','Renata','Rita','Roberta','Romana','Rosa','Rosana','Rosária','Rosário','Rute','Sabrina','Salomé','Samanta','Sandra','Sara','Silvana','Sílvia','Simone','Sofia','Solange','Sónia','Soraia','Susana','Susete','Tâmara','Tânia','Tatiana','Telma','Teresa','Valentina','Valéria','Vanda','Vanessa','Vânia','Vera','Verónica','Violeta','Virgínia','Vitória','Viviana','Zélia','Zita','Zulmira','Abílio','Adelino','Adolfo','Adriano','Afonso','Albano','Alberto','Alexandre','Alfredo','Álvaro','Américo','André','Ângelo','Aníbal','António','Armando','Armando','Artur','Augusto','Belmiro','Benjamim','Bernardo','Bruno','Caetano','Camilo','Cândido','Carlos','César','Cláudio','Cristiano','Cristóvão','Daniel','David','Delfim','Dinis','Diogo','Domingos','Duarte','Edgar','Edmundo','Eduardo','Elias','Emanuel','Emílio','Eusébio','Fábio','Fabrício','Fernando','Filipe','Flávio','Francisco','Frederico','Gabriel','Gaspar','Gil','Gilberto','Gonçalo','Gregório','Guilherme','Gustavo','Heitor','Hélder','Hélio','Henrique','Hilário','Hildebrando','Horácio','Hugo','Humberto','Igor','Inácio','Ismael','Ivan','Ivo','Jacinto','Jaime','João','Joaquim','Joel','Jorge','José','Julio','Leandro','Leonardo','Leopoldo','Lino','Lourenço','Lucas','Luciano','Lúcio','Luís','Manuel','Marcelo','Márcio','Marco','Marcos','Maria','Mário','Martim','Mateus','Matias','Maurício','Mauro','Miguel','Napoleão','Nelson','Nicolau','Norberto','Nuno','Olavo','Omar','Orlando','Óscar','Osvaldo','Otávio','Patrício','Patrício','Paulino','Paulo','Pedro','Rafael','Raimundo','Ramiro','Raul','Renato','Ricardo','Roberto','Rodolfo','Rodrigo','Rogério','Romeu','Ronaldo','Ruben','Rui','Salomão','Salvador','Samuel','Sandro','Santiago','Sebastião','Sérgio','Silvano','Silvino','Sílvio','Simão','Telmo','Teodoro','Tiago','Tomás','Tomé','Valdemar','Valentim','Valentino','Valter','Vasco','Vicente','Vítor','Vitorino','William','Xavier','Zacarias']
last_names = ['Silva','Santos','Ferreira','Pereira','Costa','Oliveira','Martins','Rodrigues','Sousa','Fernandes','Lopes','Gonçalves','Marques','Gomes','Ribeiro','Carvalho','Almeida','Pinto','Alves','Dias','Teixeira','Correia','Mendes','Soares','Vieira','Monteiro','Moreira','Cardoso','Duarte','Nunes','Rocha','Coelho','Neves','Reis','Pires','Cunha','Machado','Matos','Fonseca','Ramos','Tavares','Freitas','Simões','Cruz','Antunes','Figueiredo','Barbosa','Castro','Araújo','Azevedo','Lima','Lourenço','Faria','Morais','Andrade','Henriques','Mota','Pinheiro','Afonso','Barros','Miranda','Baptista','Melo','Guerreiro','Nogueira','Abreu','Borges','Brito','Campos','Vaz','Rosa','Maia','Esteves','Batista','Moura','Amaral','Gaspar','Leite','Jorge','Neto','Gouveia','Jesus','Branco','Valente','Pedro','Pinho','Loureiro','Filipe','Cabral','Luís','Miguel','Rebelo','Macedo','Garcia','Couto','Amorim','Nascimento','Leal','Guimarães','Vicente','Paiva','Bastos','Vasconcelos','Matias','Bento','Pacheco','Carneiro','Cordeiro','Sequeira','Domingues','Guedes','Mateus','Saraiva','Coutinho','Sampaio','Morgado','Peixoto','Manuel','Lemos','Madeira','Aguiar','Mendonça','Ramalho','Caetano','Mesquita','Serra','Godinho','Magalhes','Francisco','Paulo','Conceição','Torres','Ventura','Barata','Guerra','Domingos','Viegas','Franco','Braga','Pimenta','Bernardo','Brandão','Raposo','Freire','Pais','Trindade','Martinho','António','Magalhaes','Palma','Dinis','Veiga','Roque','Alexandre','Caldeira','Vale','Figueira','Barroso','Viana','Carreira','Carmo','Albuquerque','daSilva','Medeiros','Botelho','Pina','Lobo','Nobre','Carlos','Amaro','Inácio','Graça','Xavier','Sá','Gil','Lucas','Marinho','Cerqueira','Ferrão','Luíz','Fontes','Pimentel','Silveira','Teles','Veloso','Diogo','Ferraz','Queirós','Resende','André','Abrantes','Augusto','Barreto','Magalhães','Farinha','Calado','Patrício','Pedrosa','Louro','Meireles']

used_id_user = []
used_id_ticket = []

owned_tickets = {}

soups = ['Sopa de feijão', 'Creme de abóbora', 'Sopa de legumes', 'Sopa de nabiças', 'Sopa de couve flor', 'Sopa de penca', 'Sopa de grão-de-bico', 'Creme de tomate', 'Sopa juliana', 'Creme de legumes', 'Creme de ervilhas', 'Sopa de feijão-verde', 'Sopa de curgete', 'Caldo verde', 'Sopa de nabos', 'Sopa de brócolos', 'Sopa de alho francês', 'Sopa de penca', 'Sopa camponesa', 'Creme de cenoura', 'Sopa de couve-flor']
main_dishes = ['Frango Assado com massa cozida', 'Cachorro com batatas fritas', 'Massa com salmao', 'Badejo com arroz', 'Lasanha', 'Rancho', 'Bife de peru com batas no forno', 'Esparguete a Bolonhesa', 'Atum com massa gratinada', 'Almondegas com arroz', 'Batatas cozidas com posta de Atum', 'Feijoada com arroz', 'Rissois com batata frita', 'Pizza', 'Carne de porco a aletejana', 'Bacalhau com natas', 'Pure de batata com carne estudafa', 'Peru assado com batatas', 'Filetes de pescada panados com arroz xauxau', 'Bifes de porco grelhados com arroz de ervilha', 'Coxa de frango assado com batata frita', 'Vitela estufada com esparguete']
veg_main_dishes = ['Croquetes de milho com arroz', 'Massa de tofu', 'Rissois vejetarianos com arroz', 'Lasanha vegetariana', 'Estufado de lentilhas', 'Strogonoff de beringela', 'Cogumelos recheados com batata', 'Sufle de cenoura ', 'Berinjela recheada', 'Hamburguer de lentilha com arroz', 'Cuscuz de milho', 'Bifinhos de seitan com molho de cogumelos com massa salteada com cenoura e couve-branca', 'Pataniscas de algas com arroz colorido', 'Empadão de arroz integral com lentilhas', 'Tofu assado com pimentos', 'Caldeirada de tofu', 'Salada de Bulgur', 'Francesinha vegetariana com batata frita', 'Croquetes de seja co fussili salteada', 'Legumes salteados com tofu e massa chinesa', 'Curgete recheada com salada quente de batata', 'Strogonoff de seitan com arroz de cenoura', 'Tofu assado com pimentos batatinha no forno', 'Chili de seitan com arroz integral', 'Beringela recheada com arroz de pumentos', 'Feijoada à transmontana vegetariana com arroz integral', 'Quiche de alho francês', 'Feijoada de algas e arroz branco', 'Gratinado de legumes com cogumelos', 'Tofu de tomatada com batata cozida', 'Dhal de lentilhas', 'Feijoada de algas com arroz branco', 'Lasanha de bróocolos e requeijão', 'Pataniscas de alho-francês', 'Espetadinhas de tofu com legumes']

def new_user_name():
    return random.choice(first_names) + " " + random.choice(last_names)

def new_id_user():
    res = 'a'
    for i in range(5):
        res += str(random.randrange(10))

    if res in used_id_user:
        return new_id_user()
    else:
        used_id_user.append(res)
        return res


def register_new_user(id_user=None):
    if id_user == None:
        id_user = new_id_user()

    body = {
    	"id_user" : id_user,
    	"password" : "epah_mas_que_chatice",
    	"email" : id_user + "@alunos.uminho.pt",
    	"name": new_user_name()
    }

    response = requests.post('http://ticket-now.ddns.net:5000/api/register', json=body)

    return response.status_code == 200


def random_hex(size=8):
    hex_values = '0123456789abcdef'
    return ''.join(random.choice(hex_values) for i in range(size))

def random_id_transaction():
    characters = '0123456789'.join(string.ascii_uppercase)
    return ''.join(random.choice(characters) for i in range(18))


def make_ticket_n_transaction():
    uid = random.choice(used_id_user)
    id_ticket = random_hex(32)
    used_id_ticket.append(id_ticket)

    if not uid in owned_tickets:
        owned_tickets[uid] = []

    end = datetime.now()
    start = end - timedelta(days=100)
    transaction_date = start + (end - start) * random.random()

    
    
    owned_tickets[uid].append({ "id_ticket" : id_ticket , "bought_date" : transaction_date})

    # 1 - simple
    # 2 - complete
    mealtypes = [(1,2.05), (2,2.7)]
    mt = random.choice(mealtypes)

    res = "INSERT INTO Ticket VALUES(X'{}', '{}', {}, 0);".format(id_ticket, uid, mt[0])
    res += "\nINSERT INTO Transaction VALUES('{}', '{}', X'{}', {}, '{}');".format(random_id_transaction(), uid, id_ticket, mt[1], str(transaction_date)[:19])

    return res


def make_meals():
    td50 = timedelta(days=50)
    td1 = timedelta(days=1)

    now = date.today()
    start = now - td50
    end = now + td50

    res = ""
    date_it = start
    while (date_it < end):
        # Make azurem meals
        #   Normal
        #       Lunch
        res += "INSERT INTO Meal VALUES('{}', 1, '{}', '{}', 1, NULL);\n".format(str(date_it)[:19], random.choice(soups), random.choice(main_dishes))
        #       Dinner
        res += "INSERT INTO Meal VALUES('{}', 1, '{}', '{}', 2, NULL);\n".format(str(date_it)[:19], random.choice(soups), random.choice(main_dishes))
        #   Vegetarian
        #       Lunch
        res += "INSERT INTO Meal VALUES('{}', 1, '{}', '{}', 3, NULL);\n".format(str(date_it)[:19], random.choice(soups), random.choice(veg_main_dishes))
        #       Dinner
        res += "INSERT INTO Meal VALUES('{}', 1, '{}', '{}', 4, NULL);\n".format(str(date_it)[:19], random.choice(soups), random.choice(veg_main_dishes))
        
        # Make gualtar meals
        #   Normal
        #       Lunch
        res += "INSERT INTO Meal VALUES('{}', 2, '{}', '{}', 1, NULL);\n".format(str(date_it)[:19], random.choice(soups), random.choice(main_dishes))
        #       Dinner
        res += "INSERT INTO Meal VALUES('{}', 2, '{}', '{}', 3, NULL);\n".format(str(date_it)[:19], random.choice(soups), random.choice(veg_main_dishes))
        #   Vegetarian
        #       Lunch
        res += "INSERT INTO Meal VALUES('{}', 2, '{}', '{}', 2, NULL);\n".format(str(date_it)[:19], random.choice(soups), random.choice(main_dishes))
        #       Dinner
        res += "INSERT INTO Meal VALUES('{}', 2, '{}', '{}', 4, NULL);\n".format(str(date_it)[:19], random.choice(soups), random.choice(veg_main_dishes))
        
        date_it += td1
    
    return res


def use_some_ticket():
    uid = random.choice(used_id_user)
    if not uid in owned_tickets:
        return use_some_ticket()
    
    try:
        ticket_data_to_use = random.choice(owned_tickets[uid])
    except:
        return use_some_ticket()
        

    owned_tickets[uid].remove(ticket_data_to_use)

    end = datetime.now()
    start = ticket_data_to_use['bought_date']

    used_datetime = start + (end - start) * random.random()

    res = "UPDATE Ticket SET used = 1 WHERE id_ticket = X'{}';".format(ticket_data_to_use['id_ticket'])
    res += "INSERT INTO History VALUES('{}', X'{}', '{}');".format(used_datetime, ticket_data_to_use['id_ticket'], uid)

    return res

    



def gen_all():
    for _ in range(100):
        if not register_new_user():
            return None
    print()

    for _ in range(1000):
        print(make_ticket_n_transaction())
    print()

    print(make_meals())
    print()

    for _ in range(600):
        print(use_some_ticket())
    print()


if __name__ == '__main__':
    
    gen_all()