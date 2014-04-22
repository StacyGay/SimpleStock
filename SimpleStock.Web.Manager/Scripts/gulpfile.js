var gulp = require('gulp'),
    jshint = require('gulp-jshint'),
    uglify = require('gulp-uglify'),
    clean = require('gulp-clean'),
    concat = require('gulp-concat'),
    rename = require('gulp-rename'),
    notify = require('gulp-notify');

gulp.task('scripts', function() {
    return gulp.src('app/**/*.js')
        .pipe(concat('app.js'))
        .pipe(gulp.dest('dist'))
        .pipe(rename({ suffix: '.min' }))
        .pipe(uglify())
        .pipe(gulp.dest('dist'))
        .pipe(notify({ message: 'Scripts task complete' }));
});

gulp.task('clean', function() {
    return gulp.src(['dist'], { read: false })
        .pipe(clean());
});

gulp.task('default', ['clean'], function() {
    gulp.start('scripts');
});

gulp.task('watch', function() {
    gulp.watch('app/**/*.js', ['scripts']);
})