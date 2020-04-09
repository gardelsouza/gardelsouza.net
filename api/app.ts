//Install express server
var express = require('express');
var path = require('path');
var bodyParser = require('body-parser');
var cors = require('cors');
var send = require('sendmail.ts');

var app = express();

// Serve only the static files form the dist directory
app.use(express.static(__dirname + '/dist/app'));

// Configure body-parser
app.use(bodyParser.json()); // support json encoded bodies
app.use(bodyParser.urlencoded({ extended: true })); // support encoded bodies

// Enable cors
app.use(cors());

// Set API key to sendgrid
sgMail.setApiKey(process.env.SENDGRID_API_KEY);

// Routes
app.post('/sendMail', function (req, res) {
    if (!req.body.text) {
        return res.status(400).send({
            success: 'false',
            message: 'text is required'
        });
    }
    
    send.sendMail(req.body.text);
});

app.get('/*', function (req, res) {
    res.sendFile(path.join(__dirname + '/dist/app/index.html'));
});

// Start the app by listening on the default Heroku port
app.listen(process.env.PORT || 8080);

console.log('Running application in port 8080...')