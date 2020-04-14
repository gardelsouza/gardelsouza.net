import * as express from 'express';
import * as path  from 'path';
import * as bodyParser from 'body-parser';
import * as cors from 'cors';
import *  as send from './sendmail';

//Install express server
var app = express();

// Serve only the static files form the dist directory
app.use(express.static('./dist/app'));

// Configure body-parser
app.use(bodyParser.json()); // support json encoded bodies
app.use(bodyParser.urlencoded({ extended: true })); // support encoded bodies

// Enable cors
app.use(cors());

// Routes
app.post('/sendMail', function (req, res) {
    var ret = send.sendMail(req.body.text);

    return res.status(ret.code).send({
        success: ret.success,
        message: ret.message
    });
});

app.get('/*', function (req, res) {
    res.sendFile(path.join(__dirname + '/dist/app/index.html'));
});

// Start the app by listening on the default Heroku port
app.listen(process.env.PORT || 8080);

console.log('Running application on port 8080...')