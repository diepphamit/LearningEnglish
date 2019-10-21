import { HttpResponseBase, HttpResponse, HttpErrorResponse } from '@angular/common/http';

export class Utilities {

    public static getErrorResponses(error: HttpErrorResponse) {
        const errors: string[] = [];
        const responseObject = this.getResponseBody(error);

        if (responseObject && (typeof responseObject === 'object' || responseObject instanceof Object)) {
            for (const key in responseObject) {
                if (key) {
                    errors.push(responseObject[key]);
                } else if (responseObject[key]) {
                    errors.push(responseObject[key].toString());
                }
            }
        }

        return errors;
    }

    private static getResponseBody(response: HttpResponseBase) {
        if (response instanceof HttpResponse) {
            return response.body;
        }

        if (response instanceof HttpErrorResponse) {
            return response.error || response.message || response.statusText;
        }
    }
}
