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
 * /songs/get/{artist}/{track}:
 *    get:
 *      summary: Returns a song with wanted name and artist
 *      parameters:
 *          - in:  path
 *            name: artist
 *            schema:
 *              type: string
 *            required: true
 *            description: Name of the artist of the song
 *          - in:  path
 *            name: track
 *            schema:
 *              type: string
 *            required: true
 *            description: The song name
 *      responses: 
 *           200: 
 *                description: Song is obtained
 *                content:
 *                   "application/json":
 *                       schema:
 *                           $ref: "#components/schemas/Song"
 *           400: 
 *               description: Song not found
 *           
 *           500:
 *               description: Server error
 *          
 *       
 *      description: Get a song by track name and artist name
 */
router.get('/get/:artist/:track', GetOne)

/**
 * @swagger
 * /songs/addOne:
 *    post:
 *      summary: Add new song
 *      description: Add song to mongoDB
 *      requestBody:
 *          required: true
 *          content:
 *              application/json:
 *                  schema:
 *                      $ref: '#components/schemas/Song'
 *      responses: 
 *           200: 
 *               description: Song added successfully
 *           400: 
 *               description: Song not added. Song already exists or fields are not defined right.
 *           
 *           500:
 *               description: Server error
 *          
 *       
 */

router.post('/addOne', AddOne)

/**
 * @swagger
 * /songs/addMany:
 *    post:
 *      summary: Add new songs
 *      description: Add songs to mongoDB
 *      requestBody:
 *          required: true
 *          content:
 *              application/json:
 *                  schema:
 *                      type: array
 *                      items:
 *                          $ref: '#components/schemas/Song'
 *      responses: 
 *           200: 
 *               description: Songs added successfully
 *           400: 
 *               description: Songs not added. One of the songs already exists or fields are not defined right.
 *           
 *           500:
 *               description: Server error
 *          
 *       
 */

 
router.post('/addMany', AddMany)

router.delete('/delete/:artist/:track',DeleteOne)

router.delete('/deleteAll',DeleteAll)

router.put('/editOne',EditOne)

module.exports = router