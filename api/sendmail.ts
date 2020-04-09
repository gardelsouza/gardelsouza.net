var sgMail = require('@sendgrid/mail');

function sendMail(text: string) : any {
    var msg = {
        to: 'gardel@gardelsouza.net',
        from: 'site@gardelsouza.net',
        subject: 'Mensagem do site',
        text: text,
        html: text,
    };

    sgMail.send(msg).then(
        reason => {
            return {
                success: 'true',
                message: 'Email sent'
            };
        },
        error => {
            return {
                success: 'false',
                message: error.response.body
            };
        });

    return { "" : "" };
}

