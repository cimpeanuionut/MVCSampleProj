var Calls = {

    async GetEndpoint(apiGetRoute) {
        var urlApi = _baseUrl + apiGetRoute;

        var result = await axios
            .get(urlApi)             
            .then(response => {
                return response.data;
            }
                , err => {
                    return err.response.data;
                });

        return result;

    },

    async PostEndpoint(apiPostRoute, formData) {
        var urlApi = _baseUrl + apiPostRoute;      

        var result = await axios
            .post(urlApi,
                formData,
                {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
            .then(response => {
                return response.data;
            }
                , err => {
                    return err.response.data;
                });
        return result;
    },    
}