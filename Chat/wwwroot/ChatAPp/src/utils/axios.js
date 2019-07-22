import axios from 'axios'

const HTTP = axios.create({
    baseURL:'https://localhost:44301/api/',   
})

HTTP.setAuthorizationHeader = function(token){
    // console.log(localStorage.getItem('user-token'))
    // HTTP.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('user-token')
    // console.log(HTTP.defaults.headers)

    //HTTP.defaults.headers.common['Authorization'] = 'Bearer ' + token
    HTTP.defaults.headers.common = {'Authorization': `Bearer ${token}`}
    console.log(HTTP.defaults.headers)
}

export default HTTP
