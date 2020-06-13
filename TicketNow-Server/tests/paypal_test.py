import requests

oauth2_token='A21AAFT7PJ5p88zfNcpn7-Zzz4EpxOz9xDK5Ajrl998eOpJDMZUXyRnNS0TwR-oahrZl13mOFQ2K23YQRsAk4uCRPCxbZWU-Q'


def approve_link(order_id):
    return 'https://www.sandbox.paypal.com/checkoutnow?token={}'.format(order_id)

def status_link(order_id):
    return 'https://api.sandbox.paypal.com/v2/checkout/orders/{}'.format(order_id)

def capture_link(order_id):
    return 'https://api.sandbox.paypal.com/v2/checkout/orders/{}/capture'.format(order_id)



def gen_oauth2_token(username, password):
    url = 'https://api.sandbox.paypal.com/v1/oauth2/token?grant_type=client_credentials'
    response = requests.post(url, auth = (username, password))
    return response.json()['access_token'] if response.status_code == 200 else None

def gen_order(auth_token, price):
    url = 'https://api.sandbox.paypal.com/v2/checkout/orders'
    header = {
        'Authorization': 'Bearer ' + auth_token,
        'Content-Type': 'application/json'
        }
    data = {
        "intent": "CAPTURE",
        "purchase_units": [
            {
                "amount": {
                    "currency_code": "EUR",
                    "value": str(price)
                }
            }
        ]
    }
    response = requests.post(url, headers=header, json=data)
    
    if response.status_code == 201:
        return response.json()['id']
    else:
        return None
    



def capture_order(auth_token, order_id):
    header = {
        'Authorization': 'Bearer ' + auth_token,
        'Content-Type': 'application/json'
        }
    response = requests.post(capture_link(order_id), headers=header)
    return response.status_code == 201



username = 'AVuNdJP1b1ysHIWfqsbVvBOs57ZSC3pO7cUXjmx6X6OpCp6WWS89KdxRORun9Vp1gHzeLeqsQT5mtazx'
secret_key = 'EHOYdoXBy_acDWOpKmo0Iw3XdvG0PUJ7pVBB51D3UnwLg8umt_7MLO6__cspUUPyZdwVMxyUT6wBRbWS'

#print(gen_order(gen_oauth2_token(username, secret_key), 2.5).json())


#order = {
#   "id":"5PG02656H5857914G",
#   "links":[
#      {
#         "href":"https://api.sandbox.paypal.com/v2/checkout/orders/5PG02656H5857914G",
#         "rel":"self",
#         "method":"GET"
#      },
#      {
#         "href":"https://www.sandbox.paypal.com/checkoutnow?token=5PG02656H5857914G",
#         "rel":"approve",
#         "method":"GET"
#      },
#      {
#         "href":"https://api.sandbox.paypal.com/v2/checkout/orders/5PG02656H5857914G",
#         "rel":"update",
#         "method":"PATCH"
#      },
#      {
#         "href":"https://api.sandbox.paypal.com/v2/checkout/orders/5PG02656H5857914G/capture",
#         "rel":"capture",
#         "method":"POST"
#      }
#   ],
#   "status":"CREATED"
#}

order_id = gen_order(oauth2_token, 2.5)
print(order_id)


#print(get_approve_link(order))

'https://api.sandbox.paypal.com/v2/checkout/orders/5PG02656H5857914G'