const gulp = require("gulp");

gulp.task("css", function() {
    return gulp.src([
        "./node_modules/toastr/build/toastr.min.css"
    ])
    .pipe(gulp.dest("./wwwroot/css"));
});

gulp.task("js", function() {
    return gulp.src([
        "./node_modules/toastr/build/toastr.min.js"
    ])
    .pipe(gulp.dest("./wwwroot/js"));
});

gulp.task("default", gulp.parallel("css", "js"));