const express = require('express')
const router = express.Router()

const{
    AddOne,
    AddMany,
    DeleteOne,
    EditOne,
    GetOne,
    DeleteAll

} = require('../controllers/songsController')

/**
 * @swagger
 * /get:
 *    get:
 *      description: Get a song by track name and artist name
 */
router.get('/get/:artist/:track', GetOne)
/**
 * @swagger
 * /get:
 *    get:
 *      description: Get a song by track name and artist name
 */
router.post('/addOne', AddOne)
/**
 * @swagger
 * /get:
 *    get:
 *      description: Get a song by track name and artist name
 */
router.post('/addMany', AddMany)
/**
 * @swagger
 * /get:
 *    get:
 *      description: Get a song by track name and artist name
 */
router.delete('/delete/:artist/:track',DeleteOne)
/**
 * @swagger
 * /get:
 *    get:
 *      description: Get a song by track name and artist name
 */
router.delete('/deleteAll',DeleteAll)
/**
 * @swagger
 * /get:
 *    get:
 *      description: Get a song by track name and artist name
 */
router.put('/editOne',EditOne)
/**
 * @swagger
 * /get:
 *    get:
 *      description: Get a song by track name and artist name
 */
module.exports = router