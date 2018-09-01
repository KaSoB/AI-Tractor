import asyncio


HOST = '127.0.0.1'
PORT = 8000


async def read_data(reader):
    data = b''
    while True:
        chunk = await reader.read(64)
        if not chunk or chunk == b'':
            break
        data += chunk
    return data


async def handle_client(reader, writer):
    print("connected")
    while True:
        data = await reader.readline()
        if not data:
            print("disconnectng")
            break
        print(data)
        result = "some_text\n"
        writer.write(str(result).encode())


if __name__ == '__main__':
    loop = asyncio.get_event_loop()
    coro = asyncio.start_server(handle_client, HOST, PORT, loop=loop)
    server = loop.run_until_complete(coro)

    try:
        loop.run_forever()
    except KeyboardInterrupt:
        pass

    server.close()
    loop.run_until_complete(server.wait_closed())
    loop.close()
