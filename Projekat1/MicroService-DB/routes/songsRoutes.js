const express = require('express')
const router = express.Router()

const{
    AddOne,
    AddMany,
    GetOne

} = require('../controllers/songsController')

router.get('/get/:artist/:track', GetOne)

router.post('/addOne', AddOne)
router.post('/addMany', AddMany)

module.exports = router