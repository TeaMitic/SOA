const express = require('express')
const router = express.Router()

const{
    AddOne,
    AddMany,
    DeleteOne,
    EditOne,
    GetOne

} = require('../controllers/songsController')

router.get('/get/:artist/:track', GetOne)
router.post('/addOne', AddOne)
router.post('/addMany', AddMany)
router.delete('/delete/:artist/:track',DeleteOne)
router.put('/editOne',EditOne)

module.exports = router