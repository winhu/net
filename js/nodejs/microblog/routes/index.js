/*
 * GET home page.
 */
module.exports = function (app) {
    var crypto = require('crypto');
    var User = require('../models/user.js');
    var Post = require('../models/post.js');
    app.get('/', function (req, res) {
        Post.get(null, function(err, posts) {
            if (err) {
                posts = [];
            }
            //throw new Error('An error for test purposes.');
            res.render('index', {
                title: '首页',
                posts: posts,
            });
        });
    });

    app.get('/reg', CheckNotLogin);
    app.get('/reg', function (req, res) {
        res.render('reg', {
            title: "用户注册",
            error: req.flash('error')
        });
    });

    app.post('/reg', CheckNotLogin);
    app.post('/reg', function (req, res) {
        if (req.body['password-repeat'] != req.body['password']) {
            req.flash('error', '两次输入的口令不一致');
            return res.redirect('/reg');
        }
        var md5 = crypto.createHash('md5');
        var pwd = req.body.password;
        md5.update(pwd);
        var password = md5.digest('base64');

        var newUser = new User({
            name: req.body.username,
            password: password
        });

        User.get(newUser.name, function (err, user) {
            if (user)err = 'Username already exists';
            if (err) {
                req.flash('error', err);
                return res.redirect('/reg');
            }
            newUser.save(function (err) {
                if (err) {
                    req.flash('error', err);
                    return res.redirect('/reg');
                }
                req.session.user = newUser;
                req.flash('success', '注册成功');
                res.redirect('/');
            });
        });
    });
    app.get('/login', CheckNotLogin);
    app.get('/login', function (req, res) {
        res.render('login', {
            title: '用户登入',
        });
    });
    app.post('/login', CheckNotLogin);
    app.post('/login', function (req, res) {
//生成口令的散列值
        var md5 = crypto.createHash('md5');
        var password = md5.update(req.body.password).digest('base64');
        User.get(req.body.username, function (err, user) {
            if (!user) {
                req.flash('error', '用户不存在');
                return res.redirect('/login');
            }
            if (user.password != password) {
                req.flash('error', '用户口令错误');
                return res.redirect('/login');
            }
            req.session.user = user;
            req.flash('success', '登入成功');
            res.redirect('/');
        });
    });
    app.get('/logout', CheckLogin);
    app.get('/logout', function (req, res) {
        req.session.user = null;
        req.flash('success', '登出成功');
        res.redirect('/');
    });

    app.post('/post', CheckLogin);
    app.post('/post', function (req, res) {
        var cuser = req.session.user;
        var post = new Post(cuser.name, req.body.post);
        post.save(function (err) {
            if (err) {
                req.flash('error', err);
                return res.redirect('/');
            }
            req.flash('success', '发表成功');
            res.redirect('/u/' + cuser.name);
        });
    });

    app.get('/u/:user', function (req, res) {
        User.get(req.params.user, function (err, user) {
            if (!user) {
                req.flash('error', '用户不存在');
                return res.redirect('/');
            }
            Post.get(user.name, function (err, posts) {
                if (err) {
                    req.flash('error', err);
                    return res.redirect('/');
                }
                console.log(new Date().toLocaleTimeString());
                console.log(posts);
                res.render('user', {
                    title: user.name,
                    posts: posts
                });
            });
        });
    });

    function CheckLogin(req, res, next) {
        if (req.session.user)next();
        else {
            req.flash('error', '未登录');
            res.redirect('/login');
        }
    }

    function CheckNotLogin(req, res, next) {
        if (!req.session.user)next(); else {
            req.flash('error', '已登录');
            res.redirect('/');
        }
    }
}


exports.user = function (req, res) {
};
exports.post = function (req, res) {
};
exports.reg = function (req, res) {
};
exports.doReg = function (req, res) {
};
exports.login = function (req, res) {
};
exports.doLogin = function (req, res) {
};
exports.logout = function (req, res) {
};