export function execute(siteKey, action) {
    return new Promise((resolve, reject) => {
        grecaptcha.ready(function () {
            try {
                grecaptcha
                    .execute(siteKey, { action })
                    .then((token) => resolve(token))
                    .catch((error) => reject(error));
            } catch (e) {
                reject(e);
            }
        });
    });
}