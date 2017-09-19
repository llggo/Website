/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.filebrowserBrowseUrl = window.location.origin + "/extends/content/plugin//ckfinder/ckfinder.html";
    config.filebrowserImageBrowseUrl = window.location.origin + "/extends/content/plugin//ckfinder/ckfinder.html?Type=Images";
    config.filebrowserFlashBrowseUrl = window.location.origin + "/extends/content/plugin//ckfinder/ckfinder.html?Type=Flash";
    config.filebrowserUploadUrl = window.location.origin + "/extends/content/plugin//ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowserImageUploadUrl = window.location.origin + "/extends/content/plugin//ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = window.location.origin + "/extends/content/plugin//ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";
};
