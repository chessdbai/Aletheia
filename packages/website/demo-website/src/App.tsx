import React, { useState } from 'react';
import logo from './logo.svg';
import './App.css';
import Flexbox from 'flexbox-react';
import {Editor, EditorState} from 'draft-js';
import { AletheiaUtil } from './aletheia/AletheiaUtil';

function App() {

  var aletheia = new AletheiaUtil();

  const [pgnText, setPgnText] = React.useState('');
  const [editorMode, setEditorMode] = React.useState(
    () => true,
  );
  const [editorState, setEditorState] = React.useState(
    () => EditorState.createEmpty(),
  );

  var ele : React.ReactNode;
  if (editorMode)
  {
    ele = <textarea cols={80} rows={40} onChange={(e) => setPgnText(e.target.value!)} />;
  }
  else 
  {
    ele = <Editor editorState={editorState} onChange={setEditorState} />;
  }

  return (
    <div className="App">
      <header className="App-header">
        <Flexbox flexDirection="column" className='bp3-dark' width='100%' minHeight='100%' maxHeight='100%'>
          {ele}
        </Flexbox>
        <p>
          Put PGN text in the box above to test the PGN parser.
        </p>
        <button onClick={(e) => {
          setEditorMode(false);
          console.log(pgnText);
          aletheia.parsePgn(pgnText);
        }}>Parse PGN</button>
      </header>
    </div>
  );
}

export default App;
