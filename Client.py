#!/usr/bin/env python

import socket

TCP_IP = '127.0.0.1'
TCP_PORT = 8888
BUFFER_SIZE = 2048

client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client.connect((TCP_IP, TCP_PORT))

def send(str):
    str += '\n'
    client.send(str.encode())
def load():
    receivedBytes = client.recv(BUFFER_SIZE)
    text = receivedBytes.decode("utf-8")
    text.replace("\n", "")
    return text



print(load()) # will print "hello"
while True:
    send(input())

s.close() # unreachable
