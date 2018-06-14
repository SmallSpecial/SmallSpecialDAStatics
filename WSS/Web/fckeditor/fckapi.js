// 获取编辑器中HTML内容
function getEditorHTMLContents(EditorName) {
    var oEditor = FCKeditorAPI.GetInstance(EditorName);
    return (oEditor.GetXHTML(true));
}
// 获取编辑器中文字内容
function getEditorTextContents(EditorName) {
    var oEditor = FCKeditorAPI.GetInstance(EditorName);
    return (oEditor.EditorDocument.body.innerText);
}
// 设置编辑器中内容
function SetEditorContents(EditorName, ContentStr) {
    var oEditor = FCKeditorAPI.GetInstance(EditorName);
    oEditor.SetHTML(ContentStr);
}