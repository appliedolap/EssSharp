/**
 * This method will be called at the start of exports.transform in ManagedReference.html.primary.js
 */
exports.preTransform = function (model) {
  return model;
}

/**
 * This method will be called at the end of exports.transform in ManagedReference.html.primary.js
 */
exports.postTransform = function (model) {

  if(model.isEnum) {

    var childrens = model.children[0].children;

    childrens.forEach(function (item) {
       const regex = /[\w\d]+\s?=\s?(\d+),?/gm;
       var m = regex.exec(item.syntax.content[0].value);
       if(m !== null) {
        item._enum_value = parseInt(m[1]); 
       }
    });

    const compare = function( a, b ) {
      if ( a._enum_value < b._enum_value ){
        return -1;
      }
      if ( a._enum_value > b._enum_value ){
        return 1;
      }
      return 0;
    }

    model.children[0].children = childrens.sort(compare);
  }
  
  return model;
}