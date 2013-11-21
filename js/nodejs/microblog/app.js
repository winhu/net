/**
 * Module dependencies.
 */
var fs = require('fs');
var accessLogfile = fs.createWriteStream('access.log', {flags: 'a'});
var errorLogfile = fs.createWriteStream('error.log', {flags: 'a'});

var express = require('express');
var routes = require('./routes');
var user = require('./routes/user');
var http = require('http');
var path = require('path');
var partials = require('express-partials');
var MongoStore = require('connect-mongo')(express);
var settings = require('./settings');
var flash=require('connect-flash');
var app = express();

app.use(express.logger({stream: accessLogfile}));
// all environments
app.set('port', process.env.PORT || 3000);
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');
app.use(partials());
app.use(express.favicon());
app.use(express.logger('dev'));
app.use(express.json());
app.use(express.urlencoded());
app.use(express.methodOverride());
app.use(express.cookieParser('your secret here'));
app.use(flash());
//app.use(express.session());
app.use(express.session({
    secret:settings.cookieSecret,
    store:new MongoStore({
        db:settings.db
    })
}));
//app.locals({
//    config:config,
//    title:config.title
//});

app.use(function(req,res,next){
    res.locals.error = req.flash('error');
    res.locals.success = req.flash('success');
    res.locals.user = req.session.user;
    next();
})
//app.use(app.router);
//app.use(express.router(routes));
routes(app)
app.use(express.static(path.join(__dirname, 'public')));

// development only
if ('development' == app.get('env')) {
    app.use(express.errorHandler());
}

app.configure('production', function(){
    app.error(function (err, req, res, next) {
        var meta = '[' + new Date() + '] ' + req.url + '\n';
        errorLogfile.write(meta + err.stack + '\n');
        next();
    });
});
/*app.get('/', routes.index);
app.get('/users', user.list);

app.post('/post', routes.post);
app.get('reg', routes.reg);
app.post('reg', routes.doReg);
app.get('/login', routes.login);
app.post('/login', routes.doLogin);
app.get('/logout', routes.logout);*/




http.createServer(app).listen(app.get('port'), function () {
    console.log('Express server listening on port ' + app.get('port'));
});
