var sgMail = require('@sendgrid/mail');

sgMail.setApiKey(process.env.SENDGRID_API_KEY);

export class ApiReturnType {
    code: number;
    success: boolean;
    message: string;
}

export function sendMail(text: string): ApiReturnType {
    if (!text) {
        return {
            code: 400,
            success: false,
            message: 'text is required'
        };
    }

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
                code: 201,
                success: true,
                message: 'Email sent'
            };
        },
        error => {
            return {
                code: 401,
                success: false,
                message: error.response.body
            };
        });
}