const Net = require('net');
const port = 8888;
var i, j = 0;
var value = 0;

const clientHandler = function (socket) {
    console.log('A new connection has been established.');

    socket.on('data', function (chunk) {
        console.log(`Data received from client: ${chunk}`);
        for (i = 0; i < chunk.length; i++) {
                j++;
        }
        j = j / 2;
        value = j;
    });

    socket.write('Symbol on the string : ' + value);

    socket.on('end', function () {
        console.log('Closing connection with the client');
    });

    socket.on('error', function (err) {
        console.log(`Error: ${err}`);
    });
};

const server = new Net.Server();
server.on('connection', clientHandler);
server.listen(port, function () {
    console.log(`Server listening for connection requests on socket localhost:${port}`);
});