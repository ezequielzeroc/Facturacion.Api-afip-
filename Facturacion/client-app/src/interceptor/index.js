import fetchIntercept from 'fetch-intercept';
 
const unregister = fetchIntercept.register({
    request: function (url, config) {
        var token = localStorage.getItem("token");
        config.headers.authorization = `Bearer ${token}`;
        return [url, config];
    },
 
    requestError: function (error) {
        // Called when an error occured during another 'request' interceptor call
        return Promise.reject(error);
    },
 
    response: function (response) {
        // Modify the reponse object
        let {status} = response;
        if(status==401){
             window.location.href = '#/account/login'
        }
        return response;
    },
 
    responseError: function (error) {
        // Handle an fetch error
        return Promise.reject(error);
    }
});
