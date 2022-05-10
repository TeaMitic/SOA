const express = require('express')
const router = express.Router()

const{
    AddOne,
    AddMany,
    DeleteOne,
    EditOne

} = require('../controllers/songsController')

router.post('/addOne', AddOne)
router.post('/addMany', AddMany)
router.delete('/delete/:artist/:track',DeleteOne)
router.put('/editOne',EditOne)

module.exports = router