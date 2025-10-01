import { FormGroup } from '@angular/forms';

export class GenericValidator {
    constructor(private validationMessages: ValidationMessages) { }

    processMessages(container: FormGroup): { [key: string]: string } {
        let messages: { [key: string]: string } = {};
    
        for (let controlKey in container.controls) {
            if (container.controls.hasOwnProperty(controlKey)) {
                let c = container.controls[controlKey];
    
                if (c instanceof FormGroup) {
                    let childMessages = this.processMessages(c);
                    Object.assign(messages, childMessages);
                } else {
                    if (this.validationMessages[controlKey]) {
                        messages[controlKey] = '';
    
                        if (c.errors && (c.dirty || c.touched)) {
                            Object.keys(c.errors).forEach(messageKey => {
                                if (this.validationMessages[controlKey][messageKey]) {
                                    messages[controlKey] += this.validationMessages[controlKey][messageKey] + '<br />';
                                }
                            });
    
                            // ✅ Depuração: Verifique se o erro `underage` está sendo capturado
                            console.log(`Campo: ${controlKey}, Erros:`, c.errors);
                        }
                    }
                }
            }
        }
    
        return messages;
    }
    
}

export interface DisplayMessage {
    [key: string]: string
}
export interface ValidationMessages {
    [key: string]: { [key: string]: string }
}
