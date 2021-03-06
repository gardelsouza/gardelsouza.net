//Install express server
const express = require('express');
const path = require('path');
const bodyParser = require('body-parser');
const cors = require('cors');
const sgMail = require('@sendgrid/mail');

const app = express();

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

    const msg = {
        to: 'gardel@gardelsouza.net',
        from: 'site@gardelsouza.net',
        subject: 'Mensagem do site',
        text: req.body.text,
        html: req.body.text,
    };

    sgMail.send(msg).then(
        reason => {
            return res.status(201).send({
                success: 'true',
                message: 'Email sent'
            });
        },
        error => {
            return res.status(401).send({
                success: 'false',
                message: error.response.body
            });
        });
});

app.get('/*', function (req, res) {
    res.sendFile(path.join(__dirname + '/dist/app/index.html'));
});

// Start the app by listening on the default Heroku port
app.listen(process.env.PORT || 8080);

console.log('Running application in port 8080...')