const fs = require('fs');
const { promisify } = require('util');
const exec = promisify(require('child_process').exec)

async function makeVersion() {
  var gitContent = (await exec('git log -n 1 --pretty=format:"%H|%cD|%D"')).stdout.split('|');
  versionData = {
    gitCommitHash: gitContent[0].trim(),
    gitCommitDate: gitContent[1].trim(),
    gitRefs: gitContent[2].trim(),
	buildDate: dateGitFormat()
  }

  // git current status
  var changedFiles = (await exec("git diff --name-status")).stdout.trim();
  versionData.gitUncommittedFiles = [];
  for (let item of changedFiles.split('\n')) {
    let parts = item.split('\t');
    versionData.gitUncommittedFiles.push({
      status: parts[0],
      filename: parts[1],
    })
  }

  // write JSON to file
  fs.writeFileSync("version.json", JSON.stringify(versionData, null, 2));
}


function dateGitFormat() {
  const date = new Date();
  const yyyy = date.getFullYear();
  const mmm = date.toLocaleString('en-us', { month: 'short' });
  const dd = date.toLocaleString('en-us', { day: '2-digit' });
  const ddd = date.toLocaleString('en-us', { weekday: 'short' });
  const time = date.toLocaleTimeString();
  const tzOffset = date.getTimezoneOffset();
  const tzHours = Math.abs(tzOffset / 60).toString().padStart(2, '0');
  const tzMinutes = Math.abs(tzOffset % 60).toString().padStart(2, '0');

  return `${ddd}, ${dd} ${mmm} ${yyyy} ${time} ${tzOffset < 0 ? '+' : '-'}${tzHours}${tzMinutes}`;
}

makeVersion();