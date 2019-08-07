import axios from 'axios'

const HTTP = axios.create({
    baseURL:'https://localhost:44301/api/',   
})

HTTP.setAuthorizationHeader = function(token){   
    HTTP.defaults.headers.common = {'Authorization': `Bearer ${token}`}
    console.log('Headers!!')
    console.log(HTTP.defaults.headers)
}

function init(){//TODO: Think about init token at another place
    HTTP.defaults.headers.common = {'Authorization':`Bearer ${localStorage.getItem('user-token')}`}
}

init()
export default HTTP
