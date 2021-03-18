import { retry } from '@lifeomic/attempt';
import { ParseResult } from './Models';

interface IndexableWindow {
  [key:string]: any; // Add index signature
}

interface IDotNet {
  invokeMethodAsync: (className: string, method: string, ...args: any[]) => Promise<any>;
}

const getDotNet = () : IDotNet => {
  const realDotNet = (window as IndexableWindow)['DotNet'] as IDotNet;
  const retryWrappedDotNet : IDotNet = {
    invokeMethodAsync: async (
      className: string,
      method: string,
      ...args: any[]) => {
        return await retry(async function() {
          return realDotNet.invokeMethodAsync(className, method, ...args);
        }, {
          delay: 100,
          factor: 2,
          maxAttempts: 3
        });
      }
  };
  return retryWrappedDotNet;
};

export class AletheiaUtil {

  parsePgn = async (pgnGame: string) : Promise<ParseResult> => {
    console.log('Parsing text...');
    const dotnet = getDotNet();
    const parsedJson = await dotnet.invokeMethodAsync('Aletheia.Webasm', 'ParsePgn', pgnGame);
    console.log('Result:');
    console.log(parsedJson);
    return JSON.parse(parsedJson) as ParseResult;
  }
}