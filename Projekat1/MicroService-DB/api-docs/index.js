const getOne = require('./get-getOne')
const addOne = require('./post-addOne')
const addMany = require('./post-addMany')
const editOne = require('./put-editOne')
const deleteOne = require('./delete-deleteOne')
const deleteAll = require('./delete-deleteAll')
const apiDoc = require('../api-doc')

module.exports={
    ...apiDoc,
    path:{
        '/songs':{
            ...addOne,
            ...addMany,
            ...editOne,
            ...deleteAll
        },
        '/songs/{artistName}/{trackName}':{
            ...getOne,
            ...deleteOne,
        }
    }
}
