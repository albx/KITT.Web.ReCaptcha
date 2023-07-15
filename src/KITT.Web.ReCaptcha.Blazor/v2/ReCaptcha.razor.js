export function initialize(dotNetObject, selector, siteKey, tabIndex, theme, size) {
    return grecaptcha.render(selector, {
        'sitekey': siteKey,
        'tabindex': tabIndex,
        'theme': theme,
        'size': size,
        'callback': (response) => { dotNetObject.invokeMethodAsync('Success', response); },
        'expired-callback': () => { dotNetObject.invokeMethodAsync('Expired'); },
        'error-callback': () => { dotNetObject.invokeMethodAsync('Error'); }
    });
}