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
 * /api/songs/get/{artist}/{track}:
 *    get:
 *      summary: Returns a song with wanted name and artist
 *      parameters:
 *          - in:  path
 *            name: artist
 *            schema:
 *               $ref: "#components/schemas/ArtistName"
 *            required: true
 *            description: Name of the artist of the song
 *          - in:  path
 *            name: track
 *            schema:
 *               $ref: "#components/schemas/TrackName"
 *            required: true
 *            description: The song name
 *      responses: 
 *           200: 
 *                description: Song is obtained
 *                content:
 *                     application/json:
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
 * /api/songs/addOne:
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
 * /api/songs/addMany:
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

/**
 * @swagger
 * /api/songs/delete:
 *    delete:
 *      summary: Delete a song
 *      description: Delete song from mongoDB
 *      parameters:
 *          - in:  path
 *            name: artist
 *            schema:
 *               $ref: "#components/schemas/ArtistName"
 *            required: true
 *            description: Name of the artist of the song
 *          - in:  path
 *            name: track
 *            schema:
 *               $ref: "#components/schemas/TrackName"
 *            required: true
 *            description: The song name
 *      responses: 
 *           200: 
 *               description: Songs deleted successfully
 *           400: 
 *               description: Songs not deleted. Fields are not defined right.
 *           
 *           500:
 *               description: Server error
 *          
 *       
 */

router.delete('/delete/:artist/:track',DeleteOne)

/**
 * @swagger
 * /api/songs/deleteAll:
 *    delete:
 *      summary: Delete all songs
 *      description: Delete all songs from mongoDB
 *      
 *      responses: 
 *           200: 
 *               description: All songs deleted successfully.
 *           400: 
 *               description: Songs not deleted.
 *           
 *           500:
 *               description: Server error
 *          
 *       
 */
router.delete('/deleteAll',DeleteAll)

/**
 * @swagger
 * /api/songs/editOne:
 *    put:
 *      summary: Edit a song
 *      description: Edit song in mongoDB
 *      requestBody:
 *          required: true
 *          content:
 *              application/json:
 *                  schema:
 *                       $ref: '#components/schemas/Song'
 *      responses: 
 *           200: 
 *               description: Songs edited successfully
 *           400: 
 *               description: Songs not edited. Fields are not defined right.
 *           
 *           500:
 *               description: Server error
 *          
 *       
 */
router.put('/editOne',EditOne)

module.exports = router