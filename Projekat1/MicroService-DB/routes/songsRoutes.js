const express = require('express')
const router = express.Router()

const{
    AddOne,
    AddMany

} = require('../controllers/songsController')

router.post('/addOne', AddOne)
router.post('/addMany', AddMany)

module.exports = router